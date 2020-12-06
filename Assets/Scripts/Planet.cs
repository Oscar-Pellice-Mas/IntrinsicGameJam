using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "new planet", menuName ="Jam/New planet")]
public class Planet : ScriptableObject
{
    public GameObject planetPrefab;
    public GameObject[] llunes;
    public string Nom;
    public float radi;
    public long Poblacio;
    public int[] materials = new int[3];
    public tipus tipusPlaneta;
    public Faction faction;
    public int idFaction;
    public int Llunes;
    public int EdatEspecie;
    public int perillositat;
    

    public enum tipus { primitiu, basic, modern, avancat, futurista }
    
    //INFORMACIO EXTRA

    public regim Regim;
    public enum regim { democracia, dictadura, monarquia}

    

}

[CreateAssetMenu(fileName = "new faction", menuName = "Jam/New faction")]
public class Faction : ScriptableObject
{
    public int agresivitat;
    public int densitat;
    public raca especie;
    public Sprite imatge; 
    public enum raca { humans, ewoks, daleks, xenos, vortigaunts }
}


