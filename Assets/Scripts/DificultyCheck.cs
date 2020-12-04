using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DificultyCheck : MonoBehaviour
{
    public Planet planet;
    public int dificulty  = 0;
    public bool showData = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showData)
        {
            showData = false;
            Debug.Log("Nom: " + planet.Nom);
            if (dificulty > 0) {

                Debug.Log("Població: " + planet.QuantitatPoblació);
            }
            if (dificulty > 1)
            {
                Debug.Log("Regim: " + planet.Regim);
            }
            if (dificulty > 2)
            {
                Debug.Log("Edat espècie: " + planet.EdatEspecie);
            }
            if (dificulty > 3)
            {
                Debug.Log("Energia consumida: " + planet.EnergiaConsumida);
            }
            if (dificulty > 4)
            {
                Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 5)
            {
                Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 6)
            {
                Debug.Log("Nom: " + planet.Nom);
            }



        }
    }
}
