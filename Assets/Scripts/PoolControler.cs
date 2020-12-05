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

            
            //Aliats del planeta s'enfaden
            /*
            if (Contains(planetObjects[i].faction.allies, planet.faction)) 
            {
                planet.faction.agresivitat = Mathf.Lerp(planet.faction.agresivitat, 1, 0.1f);
                planet.perillositat = Mathf.Lerp(planet.perillositat, planet.faction.agresivitat, 0.1f);
            }
            */



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

    //IDEAS
    /*
     * Queficient d'amistat = QA
     * Si mates als planetes de la seva faccio el QA es redueix i si els deixes viure el QA puja
     * Les faccions tenen densitats (fins al 100%), mentre mes densitat tenen, mes poder.
     * Les llunes proveeixen un material en especific, si les petes consgueixes X material i sino no
     * Els planetes tenen materials que pots obtenir als destruir-los
     * -Materials:
     * -Metall?????
     * -Hi ha un material generi, mes gran, mes material
     * -També hi ha tipos de materials segons el tipus de planeta
     * 
     * 
     * EL NOSTRE PLANETA
     * -La poblacio pot variar segons els atacs que et fagin, el recursos que tinguis 
     *  Mentres mes aliats tinguis mes proteccio tens 
     * -Si tens molts recursos la poblacio augmenta, si augmenta la poblacio augmenta el consum i per tant has
     * proveeir mes materials (matar mes planetes)
     * -5 Tipus de planetes, començes al tipus 3 i pot pujar al 4 i 5
     * Quan estas al nivell 4 i 5 apareixen altres materials 
     * Mes tipus mes consum
     * Maxim i minim de planetes que pots matar cada ronda
     */
}

