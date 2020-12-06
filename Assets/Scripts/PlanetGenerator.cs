using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    GameManager gameManager;

    public TextAsset nameFile;
    private string[] Nom; // = new string[] { "Kedroapra", "Talvounus", "Olviri", "Ecrilia", "Oatis", "Theiter", "Chuubos", "Griyeyama", "Byria L3", "Lleron 7GVD" }; //Pool de noms
    public GameObject[] PlanetPrefab;
    public GameObject[] MoonPrefab;
    //Tamany i població
    private float RadiMin = 2439, RadiMax = 69911;
    //static float RadiTerra = 6371;
    private float MinRatio = 0.4f, MaxRatio = 3f;
    private float PoblacioPerKmTerra = 14.2885f;
    public long maxPopulation;
    //Edat espècie. Homo sapiens = 160000 anys.
    int EdatEspecieMin = 60000, EdatEspecieMax = 400000;

    //Energia i recursos. Terra: 1.6kW per persona
    //static float EnergyConsumedPerPersonMin = 0.5f; //kW
    //static float EnergyConsumedPerPersonMax = 4f; //kW

    //private const float EnergiaConsumidaMin = 0, EnergiaConsumidaMax = 0;
    //private const float recursosConsumitsPerAnyMin = 0, recursosConsumitsPerAnyMax = 0;
    //private const float perillositatMin = 0, perillositatMax = 0;
    //private const float pastaGeneradaMin = 0, pastaGeneradaMax = 0;

    //private Planet.raca especie;
    //private Planet.tipus tipusPlaneta;
    //private Planet.regim Regim;

    // Terra creator values
    private const long PoblacioInicial = 1000;
    private const int MaterialsInicials = 1000;

    private void Awake()
    {
        maxPopulation = (long)((4 * Mathf.PI * RadiMax * RadiMax)* PoblacioPerKmTerra * MaxRatio);
        gameManager = FindObjectOfType<GameManager>();
        Nom = nameFile.text.Split('\n');
    }

    public Planet GeneratePlanet()
    {
        Planet planet = ScriptableObject.CreateInstance<Planet>();
        planet.tipusPlaneta = (Planet.tipus)Random.Range(0, 4);
        planet.idFaction = Random.Range(0, gameManager.factions.Count);
        planet.faction = gameManager.factions[planet.idFaction];
        planet.planetPrefab = PlanetPrefab[Random.Range(0, PlanetPrefab.Length)];
        planet.EdatEspecie = Random.Range(EdatEspecieMin, EdatEspecieMax);
        planet.Nom = Nom[Random.Range(0, Nom.Length)];

        switch (planet.tipusPlaneta)
        {
            case Planet.tipus.primitiu:
                creaPrimitiu();
                break;
            case Planet.tipus.basic:
                creaBasic();
                break;
            case Planet.tipus.modern:
                creaModern();
                break;
            case Planet.tipus.avancat:
                creaAvançat();
                break;
            case Planet.tipus.futurista:
                creaFuturista();
                break;
            default:
                break;
        }

        //Crear un planeta primitiu
        void creaPrimitiu()
        {
            //TAMANY I POBLACIO
            planet.radi = Random.Range(RadiMin, RadiMin * 2);
            float area = 4 * Mathf.PI * planet.radi * planet.radi;
            long poblacioCalculada = (long)(area * PoblacioPerKmTerra * Random.Range(MinRatio, MaxRatio));
            planet.Poblacio = poblacioCalculada;

            //MATERIALS I CONSUM
            //Materila basic que tenen tots el planetes (20% del planeta esta fet d'aquest material)
            //Surten numeros molt alts s'haura de normalitzar
            planet.materials[0] = (int)(area * 0.2f);

            //Material mig prove de les llunes (en el cas dels primitius no tenen llunes)
            //Les llunes tenen un radi de 1/5 del planeta, i extreiem el 20% de material de l'area de la lluna
            float area_lluna = 4 * Mathf.PI * planet.radi / 5 * planet.radi / 5;
            planet.Llunes = 0;
            planet.materials[1] = (int)(planet.Llunes * area_lluna * 0.2f);

            //Materials avançats (els materials primitius no tenen avançat)


            //Perillositat
            planet.perillositat = Random.Range(0, 20);
        }
        //Crear un planeta basic
        void creaBasic()
        {
            //TAMANY I POBLACIO
            planet.radi = Random.Range(RadiMin, RadiMin * 3);
            float area = 4 * Mathf.PI * planet.radi * planet.radi;
            long poblacioCalculada = (long)(area * PoblacioPerKmTerra * Random.Range(MinRatio, MaxRatio));
            planet.Poblacio = poblacioCalculada;

            //MATERIALS I CONSUM
            //Materila basic que tenen tots el planetes (20% del planeta esta fet d'aquest material)
            //Surten numeros molt alts s'haura de normalitzar
            planet.materials[0] = (int)(area * 0.2f);

            //Material mig prove de les llunes (en el cas dels basics tenen entre 1 i 3 llunes)
            //Les llunes tenen un radi de 1/5 del planeta, i extreiem el 20% de material de l'area de la lluna
            float area_lluna = 4 * Mathf.PI * planet.radi / 5 * planet.radi / 5;
            planet.Llunes = Random.Range(1, 3);
            planet.materials[1] = (int)(planet.Llunes * area_lluna * 0.2f);
            //Materials avançats (els materials primitius no tenen avançat)

            //Perillositat
            planet.perillositat = Random.Range(0, 40);
        }
        //Crear un planeta modern
        void creaModern()
        {
            planet.radi = Random.Range(RadiMin, RadiMin * 4);
            float area = 4 * Mathf.PI * planet.radi * planet.radi;
            long poblacioCalculada = (long)(area * PoblacioPerKmTerra * Random.Range(MinRatio, MaxRatio));
            planet.Poblacio = poblacioCalculada;

            //MATERIALS I CONSUM
            //Materila basic que tenen tots el planetes (20% del planeta esta fet d'aquest material)
            //Surten numeros molt alts s'haura de normalitzar
            planet.materials[0] = (int)(area * 0.2f);

            //Material mig prove de les llunes (en el cas dels basics tenen entre 3 i 10 llunes)
            //Les llunes tenen un radi de 1/5 del planeta, i extreiem el 20% de material de l'area de la lluna
            float area_lluna = 4 * Mathf.PI * planet.radi / 5 * planet.radi / 5;
            planet.Llunes = Random.Range(1, 10);
            planet.materials[1] = (int)(planet.Llunes * area_lluna * 0.2f);

            //Perillositat
            planet.perillositat = Random.Range(0, 60);
        }
        //Crear un planeta avançat
        void creaAvançat()
        {
            planet.radi = Random.Range(RadiMin, RadiMin * 5);
            float area = 4 * Mathf.PI * planet.radi * planet.radi;
            long poblacioCalculada = (long)(area * PoblacioPerKmTerra * Random.Range(MinRatio, MaxRatio));
            planet.Poblacio = poblacioCalculada;

            //MATERIALS I CONSUM
            //Materila basic que tenen tots el planetes (20% del planeta esta fet d'aquest material)
            //Surten numeros molt alts s'haura de normalitzar
            planet.materials[0] = (int)(area * 0.2f);

            //Material mig prove de les llunes (en el cas dels basics tenen entre 10 i 20 llunes)
            //Les llunes tenen un radi de 1/5 del planeta, i extreiem el 20% de material de l'area de la lluna
            float area_lluna = 4 * Mathf.PI * planet.radi / 5 * planet.radi / 5;
            planet.Llunes = Random.Range(10, 20);
            planet.materials[1] = (int)(planet.Llunes * area_lluna * 0.2f);


            //Materila Especial (2% de l'area del planeta)
            planet.materials[2] = (int)(area * 0.02f);
            //Perillositat
            planet.perillositat = Random.Range(0, 80);
        }
        //Crear un planeta futurista
        void creaFuturista()
        {
            planet.radi = Random.Range(RadiMin, RadiMax);
            float area = 4 * Mathf.PI * planet.radi * planet.radi;
            long poblacioCalculada = (long)(area * PoblacioPerKmTerra * Random.Range(MinRatio, MaxRatio));
            planet.Poblacio = poblacioCalculada;

            //MATERIALS I CONSUM
            //Materila basic que tenen tots el planetes (20% del planeta esta fet d'aquest material)
            //Surten numeros molt alts s'haura de normalitzar
            planet.materials[0] = (int)(area * 0.2f);

            //Material mig prove de les llunes (en el cas dels basics tenen entre 20 i 30 llunes)
            //Les llunes tenen un radi de 1/5 del planeta, i extreiem el 20% de material de l'area de la lluna
            float area_lluna = 4 * Mathf.PI * planet.radi / 5 * planet.radi / 5;
            planet.Llunes = Random.Range(20, 30);
            planet.materials[1] = (int)(planet.Llunes * area_lluna * 0.2f);

            //Materila Especial (4% de l'area del planeta)
            planet.materials[2] = (int)(area * 0.04f);

            //Perillositat
            planet.perillositat = Random.Range(0, 100);
        }

        Debug.Log("El planeta te: "+planet.perillositat);
        return planet;
    }

    //Aqui haurem de passar algun dels valors del enum de tipus de planeta
    public Planet GenerateSpecificPlanet(string tipus)
    {
        Planet planet = ScriptableObject.CreateInstance<Planet>();
        planet.tipusPlaneta = (Planet.tipus)Random.Range(0, 4);
        planet.faction = gameManager.factions[Random.Range(0, gameManager.factions.Count)];

        switch (tipus)
        {
            case "primitiu":
                creaPrimitiu();
                break;
            case "basic":
                creaBasic();
                break;
            case "modern":
                creaModern();
                break;
            case "avancat":
                creaAvançat();
                break;
            case "futurista":
                creaFuturista();
                break;
            default:
                break;
        }

        //Crear un planeta primitiu
        void creaPrimitiu()
        {
            //TAMANY I POBLACIO
            planet.radi = Random.Range(RadiMin, RadiMin * 2);
            float area = 4 * Mathf.PI * planet.radi * planet.radi;
            long poblacioCalculada = (long)(area * PoblacioPerKmTerra * Random.Range(MinRatio, MaxRatio));
            planet.Poblacio = poblacioCalculada;

            //MATERIALS I CONSUM
            //Materila basic que tenen tots el planetes (20% del planeta esta fet d'aquest material)
            //Surten numeros molt alts s'haura de normalitzar
            planet.materials[0] = (int)(area * 0.2f);

            //Material mig prove de les llunes (en el cas dels primitius no tenen llunes)
            //Les llunes tenen un radi de 1/5 del planeta, i extreiem el 20% de material de l'area de la lluna
            float area_lluna = 4 * Mathf.PI * planet.radi / 5 * planet.radi / 5;
            planet.Llunes = 0;
            planet.materials[1] = (int)(planet.Llunes * area_lluna * 0.2f);

            //Materials avançats (els materials primitius no tenen avançat)


            //Perillositat
            planet.perillositat = Random.Range(0, 20);
        }
        //Crear un planeta basic
        void creaBasic()
        {
            //TAMANY I POBLACIO
            planet.radi = Random.Range(RadiMin, RadiMin * 3);
            float area = 4 * Mathf.PI * planet.radi * planet.radi;
            long poblacioCalculada = (long)(area * PoblacioPerKmTerra * Random.Range(MinRatio, MaxRatio));
            planet.Poblacio = poblacioCalculada;

            //MATERIALS I CONSUM
            //Materila basic que tenen tots el planetes (20% del planeta esta fet d'aquest material)
            //Surten numeros molt alts s'haura de normalitzar
            planet.materials[0] = (int)(area * 0.2f);

            //Material mig prove de les llunes (en el cas dels basics tenen entre 1 i 3 llunes)
            //Les llunes tenen un radi de 1/5 del planeta, i extreiem el 20% de material de l'area de la lluna
            float area_lluna = 4 * Mathf.PI * planet.radi / 5 * planet.radi / 5;
            planet.Llunes = Random.Range(1, 3);
            planet.materials[1] = (int)(planet.Llunes * area_lluna * 0.2f);
            //Materials avançats (els materials primitius no tenen avançat)

            //Perillositat
            planet.perillositat = Random.Range(0, 40);
        }
        //Crear un planeta modern
        void creaModern()
        {
            planet.radi = Random.Range(RadiMin, RadiMin * 4);
            float area = 4 * Mathf.PI * planet.radi * planet.radi;
            long poblacioCalculada = (long)(area * PoblacioPerKmTerra * Random.Range(MinRatio, MaxRatio));
            planet.Poblacio = poblacioCalculada;

            //MATERIALS I CONSUM
            //Materila basic que tenen tots el planetes (20% del planeta esta fet d'aquest material)
            //Surten numeros molt alts s'haura de normalitzar
            planet.materials[0] = (int)(area * 0.2f);

            //Material mig prove de les llunes (en el cas dels basics tenen entre 3 i 10 llunes)
            //Les llunes tenen un radi de 1/5 del planeta, i extreiem el 20% de material de l'area de la lluna
            float area_lluna = 4 * Mathf.PI * planet.radi / 5 * planet.radi / 5;
            planet.Llunes = Random.Range(1, 10);
            planet.materials[1] = (int)(planet.Llunes * area_lluna * 0.2f);

            //Perillositat
            planet.perillositat = Random.Range(0, 60);
        }
        //Crear un planeta avançat
        void creaAvançat()
        {
            planet.radi = Random.Range(RadiMin, RadiMin * 5);
            float area = 4 * Mathf.PI * planet.radi * planet.radi;
            long poblacioCalculada = (long)(area * PoblacioPerKmTerra * Random.Range(MinRatio, MaxRatio));
            planet.Poblacio = poblacioCalculada;

            //MATERIALS I CONSUM
            //Materila basic que tenen tots el planetes (20% del planeta esta fet d'aquest material)
            //Surten numeros molt alts s'haura de normalitzar
            planet.materials[0] = (int)(area * 0.2f);

            //Material mig prove de les llunes (en el cas dels basics tenen entre 10 i 20 llunes)
            //Les llunes tenen un radi de 1/5 del planeta, i extreiem el 20% de material de l'area de la lluna
            float area_lluna = 4 * Mathf.PI * planet.radi / 5 * planet.radi / 5;
            planet.Llunes = Random.Range(10, 20);
            planet.materials[1] = (int)(planet.Llunes * area_lluna * 0.2f);


            //Materila Especial (2% de l'area del planeta)
            planet.materials[2] = (int)(area * 0.02f);
            //Perillositat
            planet.perillositat = Random.Range(0, 80);
        }
        //Crear un planeta futurista
        void creaFuturista()
        {
            planet.radi = Random.Range(RadiMin, RadiMax);
            float area = 4 * Mathf.PI * planet.radi * planet.radi;
            long poblacioCalculada = (long)(area * PoblacioPerKmTerra * Random.Range(MinRatio, MaxRatio));
            planet.Poblacio = poblacioCalculada;

            //MATERIALS I CONSUM
            //Materila basic que tenen tots el planetes (20% del planeta esta fet d'aquest material)
            //Surten numeros molt alts s'haura de normalitzar
            planet.materials[0] = (int)(area * 0.2f);

            //Material mig prove de les llunes (en el cas dels basics tenen entre 20 i 30 llunes)
            //Les llunes tenen un radi de 1/5 del planeta, i extreiem el 20% de material de l'area de la lluna
            float area_lluna = 4 * Mathf.PI * planet.radi / 5 * planet.radi / 5;
            planet.Llunes = Random.Range(20, 30);
            planet.materials[1] = (int)(planet.Llunes * area_lluna * 0.2f);

            //Materila Especial (4% de l'area del planeta)
            planet.materials[2] = (int)(area * 0.04f);

            //Perillositat
            planet.perillositat = Random.Range(0, 100);
        }


        return planet;
    }

    public List<Faction> GenerateFactions()
    {
        List<Faction> factions = new List<Faction>();
        for (int i = 0; i < 5; i++)
        {
            Faction f = ScriptableObject.CreateInstance<Faction>();
            f.agresivitat = 0;
            f.densitat = 0; //Setejar un cop es creein planetes nous
            f.especie = (Faction.raca)i;
            factions.Add(f);
        }
        return factions;
    }

    public Terra GenerateTerra()
    {
        Terra terra = ScriptableObject.CreateInstance<Terra>();

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
        terra.consum[0] = 1 * (int)PoblacioInicial;
        terra.consum[1] = 0;
        terra.consum[2] = 0;

        //Tipus
        terra.tipusPlaneta = Terra.tipus.modern;

        //Faction
        GameManager manager = FindObjectOfType<GameManager>();
        terra.faction = manager.factions[Random.Range(0, manager.factions.Count)];

        //Regim
        terra.Regim = (Terra.regim)Random.Range(0, 2);

        //Creo la llista d'atacants
        terra.atacants = new List<string>();

        return terra;
    }

}