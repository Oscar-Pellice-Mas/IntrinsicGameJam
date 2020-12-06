using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PoolControler poolControler;
    PlanetGenerator planetGenerator;

    public ViewInfoPlanet viewInfo;

    private List<Planet> roundPlanets;
    private int roundCounter = 0;
    private int numPlanets = 0;
    private List<Planet> savedPlanets;

    public Terra terra;
    public List<Faction> factions;

    public int round = 1;

    public bool roundActive = false;

    private const int InitialPoolNumber = 10;
        
    void Start()
    {
        planetGenerator = GetComponent<PlanetGenerator>();
        factions = planetGenerator.GenerateFactions();
        terra = planetGenerator.GenerateTerra();

        poolControler = GetComponent<PoolControler>();
        poolControler.CreatePool(InitialPoolNumber);

        poolControler.RefreshFactions();
        for (int i = 0; i < factions.Count; i++)
        {
            Debug.Log(factions[i].densitat);
        }
       
        StartRound();
    }

    private void StartRound()
    {
        roundCounter = 0;
        savedPlanets = new List<Planet>();
        viewInfo.SetDificulty(round);

        roundPlanets = poolControler.GetRoundPool(5);
        numPlanets = roundPlanets.Count;
        viewInfo.SetData(roundPlanets[roundCounter]);
        roundActive = true;
    }

    public void NextPlanet()
    {
        poolControler.OnPlanetInteraction(roundPlanets[roundCounter], false);
        savedPlanets.Add(roundPlanets[roundCounter]);
        Debug.Log("Desicio: " + roundCounter + " Next");
        if (roundCounter+1 >= numPlanets)
        {
            RoundDone();
            return;
        }
        else
        {
            roundCounter++;
            viewInfo.SetData(roundPlanets[roundCounter]);
        }
    }

    public void DestroyPlanet()
    {
        poolControler.OnPlanetInteraction(roundPlanets[roundCounter],true);
        Debug.Log(roundCounter + " Destroyed");

        //Destroy planet

        if (roundCounter+1 >= numPlanets) {
            RoundDone();
            return;
        } else
        {
            roundCounter++;
            viewInfo.SetData(roundPlanets[roundCounter]);
        }
    }


    public void RoundDone()
    {
        roundActive = false;
        poolControler.AddPlanets(savedPlanets);
        Debug.Log("Round finished - " + savedPlanets.Count + "saved.");
        round++;
        StartRound();


    }

}
