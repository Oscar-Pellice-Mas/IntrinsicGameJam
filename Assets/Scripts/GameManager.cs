using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject homeUI;
    public GameObject planetUI;

    private PoolControler poolControler;
    public PlanetGenerator planetGenerator;
    public CameraShakeManager cameraShake;
    public ViewInfoPlanet viewInfo;
    public ViewInfoTerra viewInfoTerra;
    public Animator saveLeverAnimator;
    public Animator killPlanetButton;
    public Animator LightSpeedAnimation;
    public Image WhiteFadeScreen;
    public GameObject ExplosionObject;

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
    
    public int round = 0;

    public bool roundActive = false;
    public bool decisionMade = false;
    private const int InitialPoolNumber = 10;


    public SoundManager soundsManager;
        
    void Start()
    {
        planetUI.SetActive(true);
        homeUI.SetActive(false);

        planetGenerator = GetComponent<PlanetGenerator>();
        factions = planetGenerator.GenerateFactions();
        terra = planetGenerator.GenerateTerra();

        //Guardem la terra del principi
        terraAnterior = terra.Copy();

        poolControler = GetComponent<PoolControler>();
        poolControler.CreatePool(InitialPoolNumber);

        poolControler.RefreshFactions();

        laser1.SetActive(false);
        laser2.SetActive(false);

        StartRound();
    }

    public void StartRound()
    {
        round++;

        planetUI.SetActive(true);
        homeUI.SetActive(false);

        roundCounter = 0;
        savedPlanets = new List<Planet>();
        terraAnterior = terra.Copy();
        viewInfo.SetDificulty(round);

        poolControler.RefreshFactions();
        roundPlanets = poolControler.GetRoundPool(5);
        numPlanets = roundPlanets.Count;
        
        viewInfo.SetData(roundPlanets[roundCounter]);
        roundActive = true;
    }

    public IEnumerator NextPlanet()
    {
        saveLeverAnimator.SetBool("palancaDown", true);
        yield return new WaitForSeconds(0.5f);
        soundsManager.PlayButton();
        poolControler.OnPlanetInteraction(roundPlanets[roundCounter], false);
        savedPlanets.Add(roundPlanets[roundCounter]);
        yield return new WaitForSeconds(0.5f);
        saveLeverAnimator.SetBool("palancaDown", false);
        yield return new WaitForSeconds(0.2f);
        if (roundCounter+1 >= numPlanets)
        {
            LightSpeedAnimation.SetTrigger("goLightSpeed");
            soundsManager.PlayTravel();
            yield return new WaitForSeconds(3f);
            RoundDone();
            yield return null;
        }
        else
        {
            LightSpeedAnimation.SetTrigger("goLightSpeed");
            soundsManager.PlayTravel();
            yield return new WaitForSeconds(3f);
            roundCounter++;
            viewInfo.SetData(roundPlanets[roundCounter]);
        }
    }

    public IEnumerator DestroyPlanet()
    {

        killPlanetButton.SetBool("buttonDown", true);
        yield return new WaitForSeconds(0.2f);
        soundsManager.PlayButton();
        soundsManager.PlayAlarma();
        cameraShake.StartFlicker();
        yield return new WaitForSeconds(cameraShake.GetTintDuration() / 4);


        cameraShake.StartShake(cameraShake.GetTintDuration() / 4, 5, CameraShakeManager.ShakeType.incremental);
        yield return new WaitForSeconds(cameraShake.GetTintDuration() / 4);

        laser1.transform.position = viewInfo.PlanetGO.transform.position;

        laser1.transform.LookAt(LeftCorner.transform.position, Vector3.up);
        laser1.transform.Rotate(new Vector3(0, 1, 0), 90f);

        laser2.transform.position = viewInfo.PlanetGO.transform.position;

        laser2.transform.LookAt(RightCorner.transform.position, Vector3.up);
        laser2.transform.Rotate(new Vector3(0, 1, 0), 90f);

        laser1.SetActive(true);
        laser2.SetActive(true);
        soundsManager.PlayLaser();

        cameraShake.StartShake(cameraShake.GetTintDuration() / 4, 7, CameraShakeManager.ShakeType.constant);

        yield return new WaitForSeconds(cameraShake.GetTintDuration() / 4);
        poolControler.OnPlanetInteraction(roundPlanets[roundCounter], true);

        Debug.Log(roundCounter + " Destroyed");
        cameraShake.StartShake(5f, 15, CameraShakeManager.ShakeType.decremental);

        WhiteFadeScreen.color = new Color(1,1,1,1);
        ExplosionObject.GetComponent<Animator>().SetTrigger("triggerExplosion");
        soundsManager.PlayDestruction();
        viewInfo.PlanetGO.SetActive(false);
        yield return new WaitForSeconds(cameraShake.GetTintDuration() / 4);

        laser1.SetActive(false);
        laser2.SetActive(false);

        killPlanetButton.SetBool("buttonDown", false);

        yield return new WaitForSeconds(2.5f);
        //Destroy planet
        if (roundCounter+1 >= numPlanets) {
            RoundDone();
            yield return null;
        } else
        {

            LightSpeedAnimation.SetTrigger("goLightSpeed");
            soundsManager.PlayTravel();
            yield return new WaitForSeconds(3f);
            roundCounter++;
            viewInfo.SetData(roundPlanets[roundCounter]);
        }

        yield return null;
    }


    public void RoundDone()
    {
        roundActive = false;

        poolControler.ActualitzaTerra();
        viewInfoTerra.SetDataTerra(terra, terraAnterior);

        planetUI.SetActive(false);
        homeUI.SetActive(true);

        poolControler.AddPlanets(savedPlanets);
    }

    private void Update()
    {   if(viewInfo.PlanetGO != null && laser2.activeInHierarchy)
        {
            laser1.transform.position = viewInfo.PlanetGO.transform.position;

            laser1.transform.LookAt(LeftCorner.transform.position, Vector3.up);
            laser1.transform.Rotate(new Vector3(0, 1, 0), 90f);

            laser2.transform.position = viewInfo.PlanetGO.transform.position;
            
            laser2.transform.LookAt(RightCorner.transform.position, Vector3.up);
            laser2.transform.Rotate(new Vector3(0, 1, 0), 90f);
        }

        ExplosionObject.transform.position = viewInfo.PlanetGO.transform.position;
        if (WhiteFadeScreen.color.a > 0)
        {
            WhiteFadeScreen.color = new Color(WhiteFadeScreen.color.r, WhiteFadeScreen.color.g, WhiteFadeScreen.color.b, WhiteFadeScreen.color.a-0.01f);
        }


    }

}
