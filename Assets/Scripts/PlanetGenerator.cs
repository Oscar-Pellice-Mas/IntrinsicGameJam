using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    private string[] Nom = new string[]{"Kedroapra", "Talvounus", "Olviri", "Ecrilia", "Oatis", "Theiter", "Chuubos", "Griyeyama", "Byria L3", "Lleron 7GVD"}; //Pool de noms

    //Tamany i població
    static float RadiMin = 2439, RadiMax = 69911;
    //static float RadiTerra = 6371;
    static float MinRatio = 0.25f, MaxRatio = 3f;
    static float PoblacioPerKmTerra = 14.88047f;

    //Edat espècie. Homo sapiens = 160000 anys.
    static int EdatEspecieMin = 60000, EdatEspecieMax = 400000;

    //Energia i recursos. Terra: 1.6kW per persona
    static float EnergyConsumedPerPersonMin = 0.5f; //kW
    static float EnergyConsumedPerPersonMax = 4f; //kW

    private const float EnergiaConsumidaMin = 0, EnergiaConsumidaMax = 0;
    private const float recursosConsumitsPerAnyMin = 0, recursosConsumitsPerAnyMax = 0;
    private const float perillositatMin = 0, perillositatMax = 0;
    private const float pastaGeneradaMin = 0, pastaGeneradaMax = 0;

    //private Planet.raca especie;
    private Planet.tipus tipusPlaneta;
    private Planet.regim Regim;

    [MenuItem("Planets/Generte")]
    public static Planet GeneratePlanet()
    {
        Planet planet = new Planet();

        //Poblacio
        float radi = Random.Range(RadiMin, RadiMax);
        planet.radi = radi;
        float area = 4 * Mathf.PI * radi * radi;
        float poblacioCalculada = area * PoblacioPerKmTerra * Random.Range(MinRatio, MaxRatio);
        planet.QuantitatPoblacio = poblacioCalculada;

        //Especie
        planet.EdatEspecie = Random.Range(EdatEspecieMin, EdatEspecieMax);

        //Energia i economia
        planet.EnergiaConsumida = poblacioCalculada * Random.Range(EnergyConsumedPerPersonMin, EnergyConsumedPerPersonMax);

        //Agresivitat
        planet.perillositat = Random.Range(0f, 1f);
        
        
        



        //....
        //planet.recursosConsumitsPerAny = Random.Range(recursosConsumitsPerAnyMin, recursosConsumitsPerAnyMax);
        
        //planet.pastaGenerada = Random.Range(pastaGeneradaMin, pastaGeneradaMax);
        
       
        return planet;
    }

    /*
float poblacio = 7590000000;

float ratioPoblacioArea = poblacio / area;
 Debug.Log("poblacio: " + poblacio);

Debug.Log("ratioPoblacioArea: " + ratioPoblacioArea);

Debug.Log("ratio terra: "+ratioPoblacioArea / PoblacioPerKmTerra);
*/
    //Debug.Log("poblacio calculada: "+ poblacioCalculada);
}
