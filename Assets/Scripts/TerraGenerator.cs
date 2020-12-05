using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraGenerator : MonoBehaviour
{
    private const float PoblacioInicial = 1000;
    private const float MaterialsInicials = 1000;

    public static List<Faction> GenerateFactions()
    {
        List<Faction> factions = new List<Faction>();
        for (int i = 0; i < 4; i++)
        {
            Faction f = new Faction();
            f.agresivitat = 0;
            f.densitat = 0; //Setejar un cop es creein planetes nous
            f.especie = (Faction.raca)i;
        }
        return factions;
    }

    public static Terra GenerateTerra()
    {
        Terra terra = new Terra();

        //Sprite
        //terra.planetPrefab = [Treure de la carpeta]

        //Nom
        terra.name = "HOME";

        //Poblacio
        terra.Poblacio = PoblacioInicial;

        //Materials
        terra.materials[0] = MaterialsInicials;
        terra.materials[1] = 0;
        terra.materials[2] = 0;

        //Consum
        terra.consum[0] = 1 * PoblacioInicial;
        terra.consum[1] = 0;
        terra.consum[2] = 0;

        //Tipus
        terra.tipusPlaneta = Terra.tipus.modern;

        //Faction
        terra.faction = FindObjectOfType<GameManager>().factions[Random.Range(0,4)];

        //Regim
        terra.Regim = (Terra.regim)Random.Range(0, 2);

        return terra;
    }
}
