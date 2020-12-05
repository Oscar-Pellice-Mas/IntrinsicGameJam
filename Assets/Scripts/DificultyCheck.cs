using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DificultyCheck : MonoBehaviour
{
    public Planet planet;
    public ViewInfoPlanet info;
    public int dificulty  = 0;
    public bool showData = false;
    void Start()
    {
        info.nom.enabled = false;
        info.quantitatPoblacio.enabled = false;
        info.regim.enabled = false;
        info.edat.enabled = false;
        info.energia.enabled = false;
        info.tipus.enabled = false;
        info.recursos.enabled = false;
        info.perillositat.enabled = false;
        info.dineros.enabled = false;
        info.especie.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (showData)
        {
            showData = false;
            //Debug.Log("Nom: " + planet.Nom);
            info.nom.enabled = true;
            if (dificulty > 0) {
                info.quantitatPoblacio.enabled = true;
                //Debug.Log("Població: " + planet.QuantitatPoblació);
            }
            if (dificulty > 1)
            {
                info.regim.enabled = true;
                //Debug.Log("Regim: " + planet.Regim);
            }
            if (dificulty > 2)
            {
                info.edat.enabled = true;
                //Debug.Log("Edat espècie: " + planet.EdatEspecie);
            }
            if (dificulty > 3)
            {
                info.energia.enabled = true;
                //Debug.Log("Energia consumida: " + planet.EnergiaConsumida);
            }
            if (dificulty > 4)
            {
                info.tipus.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 5)
            {
                info.recursos.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 6)
            {
                info.perillositat.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 7)
            {
                info.dineros.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 8)
            {
                info.especie.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }



        }
    }
}
