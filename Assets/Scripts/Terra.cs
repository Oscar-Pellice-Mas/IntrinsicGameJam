using System.Collections;
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
    public long[] danyAtac = new long[3];
   

    public enum tipus { primitiu, basic, modern, avancat, futurista }
    
    //INFORMACIO EXTRA

    public regim Regim;
    public enum regim { democracia, dictadura, monarquia }

}

