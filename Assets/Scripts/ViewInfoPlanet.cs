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
    public TextMeshProUGUI quantitatPoblacio;
    public TextMeshProUGUI regim;
    public TextMeshProUGUI edat;
    public TextMeshProUGUI energia;
    public TextMeshProUGUI tipus;
    public TextMeshProUGUI recursos;
    public TextMeshProUGUI perillositat;
    public TextMeshProUGUI dineros;
    public TextMeshProUGUI especie;

    public int dificulty = 0;
    public bool showData = false;

    void Start()
    {
        nom.enabled = false;
        quantitatPoblacio.enabled = false;
        regim.enabled = false;
        edat.enabled = false;
        energia.enabled = false;
        tipus.enabled = false;
        recursos.enabled = false;
        perillositat.enabled = false;
        dineros.enabled = false;
        especie.enabled = false;
    }

    public void SetData(Planet planet)
    {
        planeta = planet;
        //imatge.sprite = planeta.planetSprite;
        nom.text = planeta.Nom;    
        quantitatPoblacio.text = planeta.QuantitatPoblació.ToString();
        regim.text = planeta.Regim.ToString();
        edat.text = planeta.EdatEspecie.ToString();
        energia.text = planeta.EnergiaConsumida.ToString();
        tipus.text = planeta.tipusPlaneta.ToString();
        recursos.text = planeta.recursosConsumitsPerAny.ToString();
        perillositat.text = planeta.perillositat.ToString();
        dineros.text = planeta.pastaGenerada.ToString();
        especie.text = planeta.especie.ToString();
        showData = true;
        Debug.Log(quantitatPoblacio.text);
        Debug.Log(planeta.QuantitatPoblació);
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
                recursos.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 6)
            {
                perillositat.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 7)
            {
                dineros.enabled = true;
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
