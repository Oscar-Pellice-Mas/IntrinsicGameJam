using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewInfoPlanet : MonoBehaviour
{
    public GameManager manager;

    public GameObject PlanetGO;
    public GameObject PlanetPlaceholderA;
    public GameObject PlanetPlaceholderB;

    public float TimePerRound;
    float currentTime = 0;
    bool RoundActive = false;
    public Planet planeta;

    public Image imatge;
    public TextMeshProUGUI nom;
    public TextMeshProUGUI quantitatPoblacio;
    public TextMeshProUGUI regim;
    public TextMeshProUGUI edat;
    public TextMeshProUGUI energia;
    public TextMeshProUGUI tipus;
    //public TextMeshProUGUI recursos;
    public TextMeshProUGUI perillositat;
    //public TextMeshProUGUI dineros;
    public TextMeshProUGUI especie;
    public TextMeshProUGUI radi;
    public TextMeshProUGUI llunes;

    private int dificulty = 0;
    public bool showData = false;

    public void SetDificulty(int num)
    {
        dificulty = num;
    }


    void Start()
    {/*
        nom.enabled = true;
        quantitatPoblacio.enabled = false;
        regim.enabled = false;
        edat.enabled = false;
        energia.enabled = false;
        tipus.enabled = false;
        //recursos.enabled = false;
        perillositat.enabled = false;
        //dineros.enabled = false;
        especie.enabled = false;*/
    }

    public void SetData(Planet planet, bool startRound = true)
    {

        if (startRound)
        {
            currentTime = 0;
            RoundActive = true;
        }

        planeta = planet;
        //imatge.sprite = planeta.planetSprite;
        ShowInfo(nom, planeta.Nom);

        ShowInfo(quantitatPoblacio, planeta.Poblacio.ToString());
        ShowInfo(regim, planeta.Regim.ToString());
        ShowInfo(edat, planeta.EdatEspecie.ToString());
        //ShowInfo(energia ,planeta.EnergiaConsumida.ToString());
        ShowInfo(tipus, planeta.tipusPlaneta.ToString());
        //recursos.text = planeta.recursosConsumitsPerAny.ToString();
        ShowInfo(perillositat, planeta.perillositat.ToString());
        ShowInfo(radi, planeta.radi.ToString());
        ShowInfo(llunes, planeta.Llunes.ToString());

        //dineros.text = planeta.pastaGenerada.ToString();
        //especie.text = planeta.especie.ToString();
        showData = true;
        //Debug.Log(quantitatPoblacio.text);
        //Debug.Log(planeta.QuantitatPoblacio);
    }

    private void ShowInfo(TextMeshProUGUI component, string data)
    {
        Debug.Log("showing: "+data);
        if (string.IsNullOrEmpty(data))
        {
            component.text = "?????";
        }
        else
        {
            component.text = data;
        }
    }

    void Update()
    {
        if (RoundActive)
        {
            PlanetGO.transform.position = Vector3.Lerp(PlanetPlaceholderA.transform.position, PlanetPlaceholderB.transform.position, currentTime / TimePerRound);
            currentTime += Time.deltaTime;
            if (currentTime > TimePerRound)
            {
                if (manager != null)
                    manager.NextPlanet();
                else
                    Debug.LogError("Game manager is null.");
            }
        }
        if (showData)
        {
            showData = false;
            //Debug.Log("Nom: " + planet.Nom);
            nom.enabled = true;
            if (dificulty > 0)
            {
                quantitatPoblacio.enabled = true;
                //Debug.Log("Població: " + planet.QuantitatPoblació);
            }
            if (dificulty > 1)
            {
                regim.enabled = true;
                //Debug.Log("Regim: " + planet.Regim);
            }
            if (dificulty > 2)
            {
                edat.enabled = true;
                //Debug.Log("Edat espècie: " + planet.EdatEspecie);
            }
            if (dificulty > 3)
            {
                energia.enabled = true;
                //Debug.Log("Energia consumida: " + planet.EnergiaConsumida);
            }
            if (dificulty > 4)
            {
                tipus.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 5)
            {
                //recursos.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 6)
            {
                perillositat.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 7)
            {
                //dineros.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 8)
            {
                especie.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
        }
    }
}
