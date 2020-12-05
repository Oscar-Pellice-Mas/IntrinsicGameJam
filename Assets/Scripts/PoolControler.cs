using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControler : MonoBehaviour
{
    private GameManager gameManager;

    public List<Planet> planetPool;
    public Planet objectToPool;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void CreatePool (int num)
    {
        for (int i = 0; i < num; i++)
        {
            Planet planet = PlanetGenerator.GeneratePlanet();
            planetPool.Add(planet);
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
        if (isDestroyed)
        {

            //Aliats s'enfaden


            /*if (Contains(planetObjects[i].faction.allies, planet.faction)) 
            {
                planet.faction.agresivitat = Mathf.Lerp(planet.faction.agresivitat, 1, 0.1f);
                planet.perillositat = Mathf.Lerp(planet.perillositat, planet.faction.agresivitat, 0.1f);


            }*/



            //Enemics s'alegren


            /*if (Contains(planetObjects[i].faction.enemies, planet.faction)) 
            {
                planet.faction.agresivitat = Mathf.Lerp(planet.faction.agresivitat, 0, 0.1f);


            }*/

            //Jefe content


            //Afegir llunes al planeta del jugador




        }
        else
        {
            //Aliats s'alegren


            /*if (Contains(planetObjects[i].faction.allies, planet.faction)) 
            {
                planet.faction.agresivitat = Mathf.Lerp(planet.faction.agresivitat, 0, 0.1f);
            }*/


            //Enemics s'enfaden

            /*if (Contains(planetObjects[i].faction.enemies, planet.faction))  
            {
                planet.faction.agresivitat = Mathf.Lerp(planet.faction.agresivitat, 1, 0.1f);


            }*/


            //jefe s'enfada




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
