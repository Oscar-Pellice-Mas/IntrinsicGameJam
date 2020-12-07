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
    public Sprite[] Sprites;
    //Tamany i població
    private float RadiMin = 2439, RadiMax = 6911;
    //static float RadiTerra = 6371;
    private float MinRatio = 0.4f, MaxRatio = 3f;
    private float PoblacioPerKmTerra = 14.7f;
    public long maxPopulation;
    //Edat espècie. Homo sapiens = 160000 anys.
    int EdatEspecieMin = 60000, EdatEspecieMax = 400000;
    // Terra creator values
    private const long PoblacioInicial = 1000;
    private const int MaterialsInicials = 10000000;

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
                planet = creaPrimitiu(planet);
                break;
            case Planet.tipus.basic:
                planet = creaBasic(planet);
                break;
            case Planet.tipus.modern:
                planet = creaModern(planet);
                break;
            case Planet.tipus.avancat:
                planet = creaAvançat(planet);
                break;
            case Planet.tipus.futurista:
                planet = creaFuturista(planet);
                break;
            default:
                break;
        }

        planet.llunes = new GameObject[planet.Llunes];
        for (int i = 0; i < planet.Llunes; i++)
        {
            planet.llunes[i] = (MoonPrefab[Random.Range(0, MoonPrefab.Length)]);
            //planet.llunes[i].GetComponent<RectTransform>().sizeDelta *= Random.Range(0.5f, 0.8f);
        }

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
                planet = creaPrimitiu(planet);
                break;
            case "basic":
                planet = creaBasic(planet);
                break;
            case "modern":
                planet = creaModern(planet);
                break;
            case "avancat":
                planet = creaAvançat(planet);
                break;
            case "futurista":
                planet = creaFuturista(planet);
                break;
            default:
                break;
        }
        return planet;
    }


    //Crear un planeta primitiu
    Planet creaPrimitiu(Planet planet)
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

        for (int i =0; i < planet.Llunes; i++)
        {
            planet.llunes[i] = Instantiate(MoonPrefab[Random.Range(0, MoonPrefab.Length)]);
            planet.llunes[i].GetComponent<RectTransform>().sizeDelta *= Random.Range(0.9f, 1.2f);
        }
        //Materials avançats (els materials primitius no tenen avançat)


        //Perillositat
        //planet.perillositat = Random.Range(0, 20);
        planet.perillositat = 100;
        return planet;
    }


    Planet creaBasic(Planet planet)
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
        return planet;
    }
    
    //Crear un planeta modern
    Planet creaModern(Planet planet)
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
        return planet;
    }
    
    //Crear un planeta avançat
    Planet creaAvançat(Planet planet)
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
        return planet;
    }
    //Crear un planeta futurista
    Planet creaFuturista(Planet planet)
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
        return planet;
    }

    public List<Faction> GenerateFactions()
    {
        List<Faction> factions = new List<Faction>();
        for (int i = 0; i < 5; i++)
        {
            Faction f = ScriptableObject.CreateInstance<Faction>();
            f.agresivitat = Random.Range(-100, 100);
            f.densitat = 0; //Setejar un cop es creein planetes nous
            f.especie = (Faction.raca)i;
            f.imatge = Sprites[i];
            factions.Add(f);
        }
        return factions;
    }

    public Terra GenerateTerra()
    {
        Terra terra = ScriptableObject.CreateInstance<Terra>();

        //Sprite
        terra.planetPrefab = PlanetPrefab[10];

        //Nom
        terra.Nom = "Terra";

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
        terra.indexTipus = 2;

        //Faction
        terra.idFaction = Random.Range(0, gameManager.factions.Count);
        terra.faction = gameManager.factions[terra.idFaction];

        //Regim
        terra.Regim = (Terra.regim)Random.Range(0, 2);

        //Creo la llista d'atacants
        terra.atacants = new List<Faction>();

        return terra;
    }

}