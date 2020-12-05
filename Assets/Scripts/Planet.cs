using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "new planet", menuName ="Jam/New planet")]
public class Planet : ScriptableObject
{
    public Sprite planetSprite;

    public string Nom;
    public float radi;
    public float Poblacio = 0;
    public float[] materials = new float[3];
    public tipus tipusPlaneta;
    public Faction faction;
    public int Llunes;
    public int EdatEspecie;
    public float perillositat;
    public float radi;

    public enum tipus { primitiu, basic, modern, avancat, futurista }
    
    //INFORMACIO EXTRA

    public regim Regim;
    public enum regim { democracia, dictadura, monarquia}

    public raca especie;
    public enum raca { humans, ewoks, daleks, xenos, vortigaunts }


}

[CreateAssetMenu(fileName = "new faction", menuName = "Jam/New faction")]
public class Faction : ScriptableObject
{
    public string nom;
    public float agresivitat;
    public float densitat;

    public Faction[] allies;
    public Faction[] enemies;

}


