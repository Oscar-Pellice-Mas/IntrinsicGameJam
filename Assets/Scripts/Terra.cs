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
    public long[] materials = new long[3];
    public long[] consum = new long[3];
    public int indexTipus;
    public tipus tipusPlaneta;
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
        terra.materials = new long[3];
        terra.materials[0] = materials[0];
        terra.materials[1] = materials[1];
        terra.materials[2] = materials[2];
        terra.consum = consum;
        terra.indexTipus = indexTipus;
        terra.tipusPlaneta = tipusPlaneta;
        terra.atacants = atacants;
        terra.danyAtac = danyAtac;

        return terra;
    }
}

