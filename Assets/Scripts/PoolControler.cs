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
    private Terra terraAnterior;
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

        foreach (Planet p in planetPool)
        {
            for (int i = 0; i < gameManager.factions.Count; i++)
            {
                if (p.faction.especie == gameManager.factions[i].especie)
                {
                    Debug.Log(i);
                    count[i]++;
                    break;
                }
            }
        }
        for (int i = 0; i < gameManager.factions.Count; i++)
        {
            gameManager.factions[i].densitat = count[i] * 100 / planetPool.Count;
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
                    gameManager.factions[i].agresivitat += (int)(planet.Poblacio / planetGenerator.maxPopulation) * terra.indexTipus;
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

    public void actualitzaTerra()
    {
        float materialsRestants = 0;
        float materialsConsumits = 0;
        int valorAugment = 1;


        //MIRAR SI THAN ATACAT
        for (int i = 0; i < gameManager.factions.Count; i++)
        {
            int probabilitat = Random.Range(0, 100);
            //Son enemics
            if (gameManager.factions[i].agresivitat > 30)
            {
                //t'ataquen
                if (probabilitat > gameManager.factions[i].agresivitat)
                {
                    terra.Poblacio -= (gameManager.factions[i].densitat / 100) * terra.Poblacio;
                }
            }
        }


        for (int i = 0; i < terra.consum.Length; i++)
        {
            terra.materials[i] -= terra.consum[i];
            materialsRestants += terra.materials[i] * (i+1);
            materialsConsumits += terra.consum[i] * (i + 1);
        }
        if(materialsRestants > 2*materialsConsumits)
        {
            valorAugment = (int)(materialsRestants / materialsConsumits);
            terra.Poblacio *= valorAugment;
        }

        terra.consum[0] = (int)(terra.Poblacio * gameManager.round/5 * terra.indexTipus * 3);
        terra.consum[1] = (int)(terra.Poblacio * gameManager.round / 5 * terra.indexTipus * 2);
        terra.consum[2] = (int)(terra.Poblacio * gameManager.round / 5 * terra.indexTipus * 1);

    }
}

