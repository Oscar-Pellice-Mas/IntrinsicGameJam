using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewInfoPlanet : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject PlanetGO;
    public GameObject[] moons;
    public GameObject PlanetPlaceholderA;
    public GameObject PlanetPlaceholderB;

    public float TimePerRound;
    float currentTime = 0;
    bool RoundActive = false;
    public Planet planeta;

    public Image imatge;
    public TextMeshProUGUI nom;
    public TextMeshProUGUI poblacio;
    public TextMeshProUGUI material;
    public TextMeshProUGUI radi;
    public TextMeshProUGUI llunes;
    public TextMeshProUGUI tipus;
    public TextMeshProUGUI edatEspecie;
    public TextMeshProUGUI perillositat;
    public TextMeshProUGUI faction;
    public TextMeshProUGUI regim;
    public TextMeshProUGUI raca;
    public TextMeshProUGUI densitat;
    public TextMeshProUGUI agresivitat;

    public TextMeshProUGUI day;
    public TextMeshProUGUI planetnumber;

    private int dificulty = 0;
    public bool showData = false;

    public void SetDificulty(int num)
    {
        dificulty = num;
    }

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetData(Planet planet, bool startRound = true)
    {

        if (startRound)
        {
            currentTime = 0;
            RoundActive = true;
            Transform parent = PlanetGO.transform.parent;
            int siblingIndex = PlanetGO.transform.GetSiblingIndex();
            DestroyImmediate(PlanetGO);

            PlanetGO = Instantiate(planet.planetPrefab, parent);
            PlanetGO.transform.SetSiblingIndex(siblingIndex);
            PlanetGO.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(320, 320);
            if (moons != null)
            {
                for (int i = 0; i < moons.Length; i++)
                {
                    DestroyImmediate(moons[i]);
                }
            }
            moons = new GameObject[planet.llunes.Length];
            for (int i = 0; i < moons.Length; i++)
            {
                moons[i] = Instantiate(planet.llunes[i], parent);
                moons[i].transform.SetSiblingIndex(siblingIndex+1);
            }

            //PlanetGO.transform.localScale = planet.radi
        }

        planeta = planet;

        imatge.sprite = gameManager.factions[planeta.idFaction].imatge;

        ShowInfo(nom, planeta.Nom);
        ShowInfo(poblacio, TransformLong(planeta.Poblacio));

        string materialsString = "";
        for (int i = 0; i < planeta.materials.Length; i++)
        {
            materialsString += TransformInt(planeta.materials[i]) + " - ";
        }
        
        ShowInfo(material, materialsString);
        ShowInfo(radi, TransformInt((int)planeta.radi));
        ShowInfo(regim, planeta.Regim.ToString());
        ShowInfo(llunes, planeta.Llunes.ToString());
        ShowInfo(tipus, planeta.tipusPlaneta.ToString());
        ShowInfo(edatEspecie, TransformInt(planeta.EdatEspecie));
        ShowInfo(perillositat, planeta.perillositat.ToString());
        ShowInfo(faction,planeta.faction.especie.ToString());
        ShowInfo(regim, planeta.Regim.ToString());
        ShowInfo(raca, planeta.faction.especie.ToString());
        ShowInfo(densitat, string.Format("{0}%", planeta.faction.densitat));
        ShowInfo(agresivitat, planeta.faction.agresivitat.ToString());

        ShowInfo(day, string.Format("Day {0}", gameManager.round));
        ShowInfo(planetnumber, string.Format("Planet {0} of {1}", gameManager.roundCounter+1,gameManager.numPlanets));

        showData = true;
    }

    private void ShowInfo(TextMeshProUGUI component, string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            component.text = "?????";
        }
        else
        {
            component.text = data;
        }
    }

    private string TransformInt(int data)
    {
        string retorn = "";

        if (data / 1000 < 1)
        {
            retorn = string.Format("{0}", data);
        } else if (data / 1000000 < 1)
        {
            retorn = string.Format("{0}K", data / 1000);
        }
        else if (data / 1000000000 < 1)
        {
            retorn = string.Format("{0}M", data / 1000000);
        }
        else
        {
            retorn = string.Format("{0}B", data / 1000000000);
        }

        return retorn;
    }

    private string TransformLong(long data)
    {
        string retorn;
        if (data / 1000 < 1)
        {
            retorn = string.Format("{0}", data);
        }
        else if (data / 1000000 < 1)
        {
            retorn = string.Format("{0}K", data / 1000);
        }
        else if (data / 1000000000 < 1)
        {
            retorn = string.Format("{0}M", data / 1000000);
        }
        else 
        {
            retorn = string.Format("{0}B", data / 1000000000);
        }

        return retorn;
    }

    void Update()
    {
        if (RoundActive)
        {
            PlanetGO.transform.position = Vector3.Lerp(PlanetPlaceholderA.transform.position, PlanetPlaceholderB.transform.position, currentTime / TimePerRound);
            currentTime += Time.deltaTime;
            if (currentTime > TimePerRound)
            {
                if (gameManager != null)
                    StartCoroutine(gameManager.NextPlanet());
                else
                    Debug.LogError("Game manager is null.");
            }
            float orbit = 200;
            for (int i = 0; i < moons.Length; i++)
            {
                float angle = 360 * ((float)i / (float)moons.Length);
                angle += currentTime *  Mathf.Lerp( 0.5f, 0.2f, ((float)i / (float)moons.Length));
                moons[i].transform.position = PlanetGO.transform.position + new Vector3(orbit * Mathf.Sin(angle ), orbit * Mathf.Cos(angle), 0);
                orbit += 55;
            }
        }
        if (showData)
        {
            showData = false;
            //Debug.Log("Nom: " + planet.Nom);
            nom.enabled = true;
            if (dificulty > 0)
            {
                //quantitatPoblacio.enabled = true;
                //Debug.Log("Població: " + planet.QuantitatPoblació);
            }
            if (dificulty > 1)
            {
                regim.enabled = true;
                //Debug.Log("Regim: " + planet.Regim);
            }
            if (dificulty > 2)
            {
                //edat.enabled = true;
                //Debug.Log("Edat espècie: " + planet.EdatEspecie);
            }
            if (dificulty > 3)
            {
                //energia.enabled = true;
                //Debug.Log("Energia consumida: " + planet.EnergiaConsumida);
            }
            if (dificulty > 4)
            {
                tipus.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 5)
            {
                //recursos.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 6)
            {
                perillositat.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 7)
            {
                //dineros.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
            if (dificulty > 8)
            {
                //especie.enabled = true;
                //Debug.Log("Nom: " + planet.Nom);
            }
        }
    }
}
