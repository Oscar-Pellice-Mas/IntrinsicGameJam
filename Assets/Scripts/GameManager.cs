using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PoolControler poolControler;
    private PlanetGenerator planetGenerator;
    public CameraShakeManager cameraShake;
    public ViewInfoPlanet viewInfo;
    public Animator saveLeverAnimator;
    public Animator killPlanetButton;

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

    public IEnumerator NextPlanet()
    {
        saveLeverAnimator.SetBool("palancaDown", true);
        yield return new WaitForSeconds(0.5f);
        poolControler.OnPlanetInteraction(roundPlanets[roundCounter], false);
        savedPlanets.Add(roundPlanets[roundCounter]);
        Debug.Log("Desicio: " + roundCounter + " Next");
        yield return new WaitForSeconds(0.5f);
        //yield return new WaitForSeconds(0.2f);
        saveLeverAnimator.SetBool("palancaDown", false);
        yield return new WaitForSeconds(0.2f);
        if (roundCounter+1 >= numPlanets)
        {
            RoundDone();
            yield return null;
        }
        else
        {
            roundCounter++;
            viewInfo.SetData(roundPlanets[roundCounter]);
        }
    }

    public IEnumerator DestroyPlanet()
    {

        killPlanetButton.SetBool("buttonDown", true);
        yield return new WaitForSeconds(0.2f);

        cameraShake.StartFlicker();
        yield return new WaitForSeconds(cameraShake.GetTintDuration() / 2);
        cameraShake.StartShake();

        poolControler.OnPlanetInteraction(roundPlanets[roundCounter],true);
        Debug.Log(roundCounter + " Destroyed");

        yield return new WaitForSeconds(cameraShake.GetTintDuration() / 2);

        killPlanetButton.SetBool("buttonDown", false);

        yield return new WaitForSeconds(0.2f);
        //Destroy planet

        if (roundCounter+1 >= numPlanets) {
            RoundDone();
            yield return null;
        } else
        {
            roundCounter++;
            viewInfo.SetData(roundPlanets[roundCounter]);
        }

        yield return null;
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
