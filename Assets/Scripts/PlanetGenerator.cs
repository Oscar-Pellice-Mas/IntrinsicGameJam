﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    private List<string> Nom; //Pool de noms
    private Planet.raca especie;
    private Planet.tipus tipusPlaneta;
    private Planet.regim Regim;
    
    private const float QuantitatPoblacióMin = 0, QuantitatPoblacióMax = 1000;
    private const int EdatEspecieMin = 0, EdatEspecieMax = 1000;
    private const float EnergiaConsumidaMin = 0, EnergiaConsumidaMax = 1000;
    private const float recursosConsumitsPerAnyMin = 0, recursosConsumitsPerAnyMax = 1000;
    private const float perillositatMin = 0, perillositatMax = 1000;
    private const float pastaGeneradaMin = 0, pastaGeneradaMax = 1000;
    

    public static Planet GeneratePlanet()
    {
        Planet planet = new Planet();
        planet.Nom = "Test";
        planet.QuantitatPoblació = Random.Range(QuantitatPoblacióMin, QuantitatPoblacióMax);
        planet.EdatEspecie = Random.Range(EdatEspecieMin, EdatEspecieMax);
        planet.EnergiaConsumida = Random.Range(EnergiaConsumidaMin, EnergiaConsumidaMax);
        planet.recursosConsumitsPerAny = Random.Range(recursosConsumitsPerAnyMin, recursosConsumitsPerAnyMax);
        planet.perillositat = Random.Range(perillositatMin, perillositatMax);
        planet.pastaGenerada = Random.Range(pastaGeneradaMin, pastaGeneradaMax);
        return planet;
    } 

    
}
