using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Terra", menuName = "Jam/Terra")]
public class Terra : ScriptableObject
{
    public GameObject planetPrefab;

    public string Nom;
    public float Poblacio = 0;
    public float[] materials = new float[3];
    public float[] consum = new float[3];
    public tipus tipusPlaneta;
    public Faction faction;
   

    public enum tipus { primitiu, basic, modern, avancat, futurista }
    
    //INFORMACIO EXTRA

    public regim Regim;
    public enum regim { democracia, dictadura, monarquia }


}

