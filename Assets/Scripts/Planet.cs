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
    public long[] materials = new long[3];
    public tipus tipusPlaneta;
    public int indexTipus;
    public Faction faction;
    public int idFaction;
    public int Llunes;
    public int EdatEspecie;
    public int perillositat;
    
    public enum tipus { Primitive, Basic, Modern, Advanced, Futurist }   
}

[CreateAssetMenu(fileName = "new faction", menuName = "Jam/New faction")]
public class Faction : ScriptableObject
{
    public int agresivitat;
    public int mitjaPerillositat;
    public int densitat;
    public raca especie;
    public Sprite imatge; 
    public enum raca { humans, ewoks, daleks, xenos, vortigaunts }
}


