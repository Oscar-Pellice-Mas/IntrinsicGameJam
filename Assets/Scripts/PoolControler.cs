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
            gameManager.factions[i].mitjaPerillositat = gameManager.factions[i].mitjaPerillositat / count[i];
        }

        foreach (Planet p in planetPool)
        {
            if (gameManager.factions[p.idFaction].mitjaPerillositat > 50)
            {
                p.perillositat += 1;
            } else
            {
                p.perillositat -= 1;
            }
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
        RefreshFactions();

        // Mirem si s'ha destruit
        if (isDestroyed)
        {
            foreach (Planet p in planetPool)
            {
                if (p.faction == planet.faction)
                {
                    p.perillositat += 1;
                }
            }

            //Sumem els valors materials
            for (int i = 0; i < planet.materials.Length; i++)
            {
                terra.materials[i] += planet.materials[i];
            }

            //Agressivitat de la faccio augmenta
            for (int i = 0; i < gameManager.factions.Count; i++)
            {
                if (planet.faction.especie == gameManager.factions[i].especie)
                {
                    //Calculem la agresivitat que generem en una faccio al matar un dels seus planetes
                    // agresivitat = (poblacio / (poblacioMax/5) ) 
                    gameManager.factions[i].agresivitat += (int)(planet.Poblacio / (planetGenerator.maxPopulation / 100) + 1) * (planet.indexTipus+1);
                    break;
                }
            }
        }
        else
        {
            foreach (Planet p in planetPool)
            {
                if (p.faction == planet.faction)
                {
                    p.perillositat += -1;
                }
            }
            for (int i = 0; i < gameManager.factions.Count; i++)
            {
                if (planet.faction.especie == gameManager.factions[i].especie)
                {
                    //Calculem la agresivitat que generem en una faccio al matar un dels seus planetes
                    gameManager.factions[i].agresivitat -= (int)(planet.Poblacio / (planetGenerator.maxPopulation / 10)) * planet.indexTipus;
                    break;
                }

            }

        }

        gameManager.terra = terra;
    }

    public void ActualitzaTerra()
    {
        terra = gameManager.terra;

        terra.indexTipus = Mathf.Clamp(gameManager.round/5 + 2,2,4);
        terra.tipusPlaneta = (Terra.tipus)terra.indexTipus;

        long materialsRestants = 0;
        long materialsConsumits = 0;

        int probabilitat;
        int attack;

        terra.atacants = new List<Faction>();
        terra.danyAtac = new List<long>();

        // Restem els conusums als recursos que tenim
        for (int i = 0; i < terra.consum.Length; i++)
        {
            terra.materials[i] -= terra.consum[i];
            materialsRestants += terra.materials[i] * (i + 1);
            materialsConsumits += terra.consum[i] * (i + 1);
        }

        // Si hi ha materials suficients per fer creixer el planeta
        if (materialsRestants > 2 * materialsConsumits)
        {
            // Augmentem la poblacio
            long valorAugment = materialsRestants / (2 * materialsConsumits);
            terra.Poblacio += (long)(terra.Poblacio * valorAugment * 0.01);
        }
        else
        {
            long valorAugment = materialsRestants / (2 * materialsConsumits);
            terra.Poblacio -= (long)(terra.Poblacio * valorAugment * 0.01);
        }
        // Depenent de la ronda, recalculem diferents consums
        switch (terra.indexTipus){
            case 1:
                terra.consum[0] = terra.Poblacio * 50;
                terra.consum[1] = terra.Poblacio * 30;
                terra.consum[2] = terra.Poblacio * 10;
                break;
            case 2:
                terra.consum[0] = terra.Poblacio * 60;
                terra.consum[1] = terra.Poblacio * 40;
                terra.consum[2] = terra.Poblacio * 20;
                break;
            case 3:
                terra.consum[0] = terra.Poblacio * 70;
                terra.consum[1] = terra.Poblacio * 50;
                terra.consum[2] = terra.Poblacio * 30;
                break;
            case 4:
                terra.consum[0] = terra.Poblacio * 80;
                terra.consum[1] = terra.Poblacio * 60;
                terra.consum[2] = terra.Poblacio * 40;
                break;
        }

        //Primer mirerm les faccions
        for (int i = 0; i < gameManager.factions.Count; i++)
        {
            if (gameManager.factions[i].densitat == 0) continue; //Si no tenen ningu no ataquen
            if (terra.atacants.Count == 4) break;
            probabilitat = Random.Range(0, 100); //Calculem l'atack
            //Mirem si son enemics
            if (gameManager.factions[i].agresivitat > 30) //Si no es agresiu no atacara
            {
                // Calculem el rang del atac
                // Atac = agresivitat [-100/100] * perillositat [0/100] / 100;  Queda sempre un valor entre 0 i 100
                attack = gameManager.factions[i].agresivitat + gameManager.factions[i].mitjaPerillositat / 2; //Revisar valors
                if (probabilitat < attack) //Si supera el atac
                {
                    //Calculem el dany que rebrem: densitat [0/100] * poblacio / 100; Resulta un valor entre 0 i el maxim de la teva poblacio 
                    long dany = -((gameManager.factions[i].densitat+25) * terra.Poblacio) / 100;
                    
                    //ens apuntem qui ens ha atacat
                    terra.danyAtac.Add(dany);
                    terra.atacants.Add(gameManager.factions[i]);
                }
            }
        }

        // Restem el atac
        for (int i = 0; i < terra.danyAtac.Count; i++)
        {
            terra.Poblacio += terra.danyAtac[i];
        }
        

        gameManager.terra = terra;
    }
}

