using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControler : MonoBehaviour
{
    private GameManager gameManager;
    private PlanetGenerator planetGenerator;

    public List<Planet> planetPool;
    public Planet objectToPool;

    private Terra terra;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        planetGenerator = GetComponent<PlanetGenerator>();
    }

    public void CreatePool (int num)
    {
        for (int i = 0; i < num; i++)
        {
            Planet planet = planetGenerator.GeneratePlanet();
            planetPool.Add(planet);
        }
    }

    public void RefreshFactions()
    {
        int[] count = new int[5];
        foreach(Faction f in gameManager.factions) f.mitjaPerillositat = 0;

        foreach (Planet p in planetPool)
        {
            for (int i = 0; i < gameManager.factions.Count; i++)
            {
                if (p.faction.especie == gameManager.factions[i].especie)
                {
                    gameManager.factions[i].mitjaPerillositat += p.perillositat;
                    count[i]++;
                    break;
                }
            }
        }

        for (int i = 0; i < gameManager.factions.Count; i++)
        {
            gameManager.factions[i].densitat = count[i] * 100 / planetPool.Count;
            //gameManager.factions[i].mitjaPerillositat = gameManager.factions[i].mitjaPerillositat / count[i];
        }
    }

    public List<Planet> GetRoundPool(int num)
    {
        List<Planet> planetList = new List<Planet>();

        if (num > planetPool.Count) CreatePool(num);

        for (int i = 0; i < num; i++)
        {
            int randomInt = Random.Range(0, planetPool.Count - 1);
            Planet p = planetPool[randomInt];
            planetList.Add(p);
            planetPool.RemoveAt(randomInt);
        }

        return planetList;
    }

    public void AddPlanets(List<Planet> planets)
    {
        foreach(Planet p in planets){
            planetPool.Add(p);
        }
    }

    public void OnPlanetInteraction(Planet planet, bool isDestroyed)
    {
        terra = gameManager.terra;
        
        if (isDestroyed)
        {
            //Actualizar valors materials
            for(int i = 0; i < planet.materials.Length; i++)
            {
                terra.materials[i] += planet.materials[i];
            }

            //Agressivitat de la faccio
            for (int i = 0; i < gameManager.factions.Count; i++)
            {
                if (planet.faction.especie == gameManager.factions[i].especie)
                {
                    
                    //Calculem la agresivitat que generem en una faccio al matar un dels seus planetes
                    gameManager.factions[i].agresivitat += (int)(planet.Poblacio / planetGenerator.maxPopulation) * terra.indexTipus;
                    break;
                }

            }


        }
        else
        {
            for (int i = 0; i < gameManager.factions.Count; i++)
            {
                if (planet.faction.especie == gameManager.factions[i].especie)
                {

                    //Calculem la agresivitat que generem en una faccio al matar un dels seus planetes
                    gameManager.factions[i].agresivitat -= (int)(planet.Poblacio / planetGenerator.maxPopulation) * terra.indexTipus;
                    break;
                }

            }

        }
        gameManager.terra = terra;
    }

    public bool Contains(Faction[] factions, Faction faction)
    {
        for(int i = 0; i < factions.Length; i++)
        {
            if (factions[i] == faction)
            {
                return true;
            }
        }

        return false;
    }

    public void ActualitzaTerra()
    {
        terra = gameManager.terra;

        long materialsRestants = 0;
        long materialsConsumits = 0;

        int probabilitat;
        int attack;

        terra.atacants = new List<Faction>();
        terra.danyAtac = new List<long>();

        //Primer mirerm si al tornar
        for (int i = 0; i < gameManager.factions.Count; i++)
        {
            if (gameManager.factions[i].densitat == 0) continue;
            if (gameManager.factions[i].especie == Faction.raca.humans) continue;
            probabilitat = Random.Range(0, 100);
            //Son enemics
            if (gameManager.factions[i].agresivitat > 30)
            {
                //t'ataquen
                attack = gameManager.factions[i].agresivitat * gameManager.factions[i].mitjaPerillositat / 100;
                if (probabilitat < attack)
                {
                    long dany = -(gameManager.factions[i].densitat * terra.Poblacio) / 100;
                    terra.Poblacio += dany;
                    //ens apuntem qui ens ha atacat
                    terra.danyAtac.Add(dany);
                    terra.atacants.Add(gameManager.factions[i]);
                }
            }
        }


        for (int i = 0; i < terra.consum.Length; i++)
        {
            terra.materials[i] -= terra.consum[i];
            materialsRestants += terra.materials[i] * (i + 1);
            materialsConsumits += terra.consum[i] * (i + 1);
        }

        if (materialsRestants > 2 * materialsConsumits)
        {

            long valorAugment = materialsRestants / (2 * materialsConsumits);
            terra.Poblacio += (long)(terra.Poblacio*valorAugment*0.01);
            //terra.Poblacio *= valorAugment;
        }
        
        if (terra.indexTipus >= 2)
        {
            terra.consum[0] = terra.Poblacio*50;
            terra.consum[1] = terra.Poblacio * 30;
            terra.consum[2] = terra.Poblacio * 10;
        }
        if (terra.indexTipus >= 4)
        {
            terra.consum[0] = terra.Poblacio * 10;
            terra.consum[1] = (long)(terra.Poblacio * 30);
            terra.consum[2] = terra.Poblacio * 50;
        }
        //if (terra.indexTipus >= 2) terra.consum[0] = (terra.Poblacio * (gameManager.round / 5 + 1) * 3);
        //if (terra.indexTipus >= 4) terra.consum[2] = (terra.Poblacio * (gameManager.round / 5 + 1) * 1);

        gameManager.terra = terra;
    }
}

