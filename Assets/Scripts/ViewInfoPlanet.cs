using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewInfoPlanet : MonoBehaviour
{
    public Planet planeta;

    public Image imatge;
    public TextMeshProUGUI nom;
    public TextMeshProUGUI poblacio;
    public TextMeshProUGUI material;
    public TextMeshProUGUI llunes;
    public TextMeshProUGUI tipus;
    public TextMeshProUGUI edatEspecie;
    public TextMeshProUGUI perillositat;
    public TextMeshProUGUI faction;
    public TextMeshProUGUI regim;
    public TextMeshProUGUI raca;
    public TextMeshProUGUI densitat;
    public TextMeshProUGUI agresivitat;

    private int dificulty = 0;
    public bool showData = false;

    public void SetDificulty(int num)
    {
        dificulty = num;
    }

    void Start()
    {/*
        nom.enabled = true;
        poblacio.enabled = false;
        material.enabled = false;
        llunes.enabled = false;
        tipus.enabled = false;
        edatEspecie.enabled = false;
        perillositat.enabled = false;
        faction.enabled = false;
        regim.enabled = false;
        raca.enabled = false;
        densitat.enabled = false;
        agresivitat.enabled = false;
        */
    }

    public void SetData(Planet planet)
    {
        planeta = planet;
        //imatge.sprite = planeta.planetSprite;
        ShowInfo(nom, planeta.Nom);

        //ShowInfo(quantitatPoblacio, planeta.Poblacio.ToString());
        ShowInfo(regim, planeta.Regim.ToString());
        //ShowInfo(edat, planeta.EdatEspecie.ToString());
        //ShowInfo(energia ,planeta.EnergiaConsumida.ToString());
        ShowInfo(tipus, planeta.tipusPlaneta.ToString());
        //recursos.text = planeta.recursosConsumitsPerAny.ToString();
        ShowInfo(perillositat, planeta.perillositat.ToString());
        //ShowInfo(radi, planeta.radi.ToString());
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
        if (showData)
        {
            showData = false;
            //Debug.Log("Nom: " + planet.Nom);
            nom.enabled = true;
            if (dificulty > 0)
            {
                //quantitatPoblacio.enabled = true;
                //Debug.Log("Població: " + planet.QuantitatPoblació);
            }
            if (dificulty > 1)
            {
                regim.enabled = true;
                //Debug.Log("Regim: " + planet.Regim);
            }
            if (dificulty > 2)
            {
                //edat.enabled = true;
                //Debug.Log("Edat espècie: " + planet.EdatEspecie);
            }
            if (dificulty > 3)
            {
                //energia.enabled = true;
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
                //especie.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
        }
    }
}
