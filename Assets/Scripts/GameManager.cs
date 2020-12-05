using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PoolControler poolControler;
    
    public ViewInfoPlanet viewInfo;

    private List<Planet> roundPlanets;
    private int roundCounter = 0;
    private int numPlanets = 0;
    private List<Planet> savedPlanets;

    private int round = 1;

    public bool roundActive = false;

    private const int InitialPoolNumber = 10;
        
    void Start()
    {
        poolControler = GetComponent<PoolControler>();
        poolControler.CreatePool(InitialPoolNumber);

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
        Debug.Log(roundCounter + " Destroyed");
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
