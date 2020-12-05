using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "new planet", menuName ="Jam/New planet")]
public class Planet : ScriptableObject
{
    public Sprite planetSprite;


    public string Nom;
    public float QuantitatPoblacio = 0;
    public int LlunesExplotades;
    public int EdatEspecie;
    public float EnergiaConsumida;
    public float perillositat;
    
    public Faction faction;

    public tipus tipusPlaneta;
    public float recursosConsumitsPerAny;
    public float pastaGenerada;    
    public regim Regim;

    public enum tipus { primitiu, avancat, futurista }
    public enum regim { democracia, dictadura, monarquia}
}

[CreateAssetMenu(fileName = "new faction", menuName = "Jam/New faction")]
public class Faction : ScriptableObject
{
    public string nom;
    public raca especie;
    public float agresivitat;

    public Faction[] allies;
    public Faction[] enemies;

    public enum raca { humans, ewoks, daleks, xenos, vortigaunts }
}
/*
    
    Petar planeta:
    pros:
    - el teu planeta pot minar les llunes
    - jefe content
    
    cons:
    - aliats s'enfaden ->   puja agresivitat dels aliats. (mateixa raça)
    - 


    ignorar planeta
    pros:
    - agresivitat baixa (per ell i aliats)
    - aporten recursos
    

    cons:
    - et poden atacar si són agresius.
    - jefe s'enfada si l'havies de rebentar

*/

