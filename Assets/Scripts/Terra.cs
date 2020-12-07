﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Terra", menuName = "Jam/Terra")]
public class Terra : ScriptableObject
{
    public GameObject planetPrefab;

    public string Nom;
    public long Poblacio = 0;
    public int[] materials = new int[3];
    public int[] consum = new int[3];
    public int indexTipus;
    public tipus tipusPlaneta;
    public Faction faction;
    public int idFaction;

    public List<Faction> atacants;
    public List<long> danyAtac;
   

    public enum tipus { primitiu, basic, modern, avancat, futurista }
    
    //INFORMACIO EXTRA

    public regim Regim;
    public enum regim { democracia, dictadura, monarquia }

    public Terra Copy()
    {
        Terra terra = ScriptableObject.CreateInstance<Terra>();
        terra.Nom = Nom;
        terra.Poblacio = Poblacio;
        terra.materials = new int[3];
        terra.materials[0] = materials[0];
        terra.materials[1] = materials[1];
        terra.materials[2] = materials[2];
        terra.consum = consum;
        terra.indexTipus = indexTipus;
        terra.tipusPlaneta = tipusPlaneta;
        terra.faction = faction;
        terra.idFaction = idFaction;
        terra.atacants = atacants;
        terra.danyAtac = danyAtac;

        return terra;
    }
}

