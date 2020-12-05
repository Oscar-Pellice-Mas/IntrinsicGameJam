using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControler : MonoBehaviour
{
    private PoolControler SharedInstance;
    private GameManager gameManager;

    public List<Planet> planetObjects;
    public Planet objectToPool;


    private void Awake()
    {
        SharedInstance = this;
        gameManager = FindObjectOfType<GameManager>();
    }

    public void CreatePool (int num)
    {
        for (int i = 0; i < num; i++)
        {
            Planet planet = PlanetGenerator.GeneratePlanet();
            planetObjects.Add(planet);
        }
    }

    public Planet GetPooledObject()
    {
        Planet obj = null;
        if (planetObjects.Count > 0)
        {
            obj = planetObjects[0];
            planetObjects.RemoveAt(0);
        }
        if (planetObjects.Count == 0) gameManager.RoundDone();
        return obj;
    }

    public void OnPlanetInteraction(Planet planet, bool isDestroyed)
    {
        for (int i = 0; i < planetObjects.Count; i++)
        {
            if (planetObjects[i] != planet)
            {
                if (isDestroyed)
                {
                    if (Contains(planetObjects[i].faction.allies, planet.faction)) //isAlly
                    {

                    }else if (Contains(planetObjects[i].faction.enemies, planet.faction)) //isenemy
                    {

                    }
                }
                else
                {

                }

            }
        }


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

}
