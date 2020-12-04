using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "new plnet", menuName ="Jam/New planet")]
public class Planet : ScriptableObject
{
    public Sprite planetSprite;


    public string Nom;
    public float QuantitatPoblació = 0;
    public regim Regim;
    public int EdatEspecie;
    public float EnergiaConsumida;
    public tipus tipusPlaneta;
    public float recursosConsumitsPerAny;
    public float perillositat;
    public float pastaGenerada;
    public raca especie;




    public enum raca { humans, reptilians, ewoks}
    public enum tipus { primitiu, avancat, futurista }
    public enum regim { democracia, dictadura, monarquia}
}
