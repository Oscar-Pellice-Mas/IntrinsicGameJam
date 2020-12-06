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

    public GameObject laser1;
    public GameObject laser2;

    public GameObject LeftCorner;
    public GameObject RightCorner;

    private List<Planet> roundPlanets;
    public int roundCounter = 0;
    public int numPlanets = 0;
    private List<Planet> savedPlanets;

    public Terra terra;
    public Terra terraAnterior;

    public List<Faction> factions;

    public int round = 1;

    public bool roundActive = false;

    private const int InitialPoolNumber = 10;
        
    void Start()
    {
        planetGenerator = GetComponent<PlanetGenerator>();
        factions = planetGenerator.GenerateFactions();
        terra = planetGenerator.GenerateTerra();
        //Guardem la terra del principi
        terraAnterior = terra;
        poolControler = GetComponent<PoolControler>();
        poolControler.CreatePool(InitialPoolNumber);

        poolControler.RefreshFactions();
        for (int i = 0; i < factions.Count; i++)
        {
            Debug.Log(factions[i].densitat);
        }

        laser1.SetActive(false);
        laser2.SetActive(false);

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
        yield return new WaitForSeconds(cameraShake.GetTintDuration() / 4);


        cameraShake.StartShake();
        yield return new WaitForSeconds(cameraShake.GetTintDuration() / 4);

        laser1.SetActive(true);
        laser2.SetActive(true);


        yield return new WaitForSeconds(cameraShake.GetTintDuration() / 4);
        poolControler.OnPlanetInteraction(roundPlanets[roundCounter],true);
        Debug.Log(roundCounter + " Destroyed");


        yield return new WaitForSeconds(cameraShake.GetTintDuration() / 4);

        laser1.SetActive(false);
        laser2.SetActive(false);

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
        //Creem un script amb tota la informacio que necessitem de la terra al final i al començar la ronda
        RoundInfo roundInfo = new RoundInfo();
        //Mostrem la info

        //Canviar la terra anterior per guardar els canvis
        terraAnterior = terra;
        StartRound();


    }

    private void Update()
    {   if(viewInfo.PlanetGO != null && laser2.activeInHierarchy)
        {
            Debug.LogError("ei");
            laser1.transform.position = viewInfo.PlanetGO.transform.position;

            laser1.transform.LookAt(LeftCorner.transform.position, Vector3.up);
            laser1.transform.Rotate(new Vector3(0, 1, 0), 90f);

            laser2.transform.position = viewInfo.PlanetGO.transform.position;
            
            laser2.transform.LookAt(RightCorner.transform.position, Vector3.up);
            laser2.transform.Rotate(new Vector3(0, 1, 0), 90f);
        }
        
    }
    public void generaInfoRonda()
    {
        RoundInfo roundInfo = new RoundInfo();
        //Agafar els valors de poblacio la terra nova i antiga
        roundInfo.poblacio[0] = terraAnterior.Poblacio;
        roundInfo.poblacio[1] = terra.Poblacio;

        //Agafar els materials
        roundInfo.materials_abans = terraAnterior.materials;
        roundInfo.materials_ara = terra.materials;

        //Agafar els materials
        roundInfo.consum_abans = terraAnterior.consum;
        roundInfo.consum_ara = terra.consum;

        roundInfo.atacants = terra.atacants;
    }

}
