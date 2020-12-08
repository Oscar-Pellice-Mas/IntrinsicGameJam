using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewInfoPlanet : MonoBehaviour
{
    public GameManager gameManager;
    public Animator Background;
    public GameObject PlanetGO;
    public GameObject[] moons;
    public float[] moonPhase;
    public GameObject PlanetPlaceholderA;
    public GameObject PlanetPlaceholderB;

    public float TimePerRound;
    float currentTime = 0;
    public bool RoundActive = false;
    public Planet planeta;

    // UI PLANETA
    public Image boleta;
    public Image boletaPlaceholderStart;
    public Image boletaPlaceholderEnd;

    public Image imatge;
    public TextMeshProUGUI nom;
    public TextMeshProUGUI poblacio;
    public TextMeshProUGUI tipus;
    public TextMeshProUGUI perillositat;
    public TextMeshProUGUI materialEnemic;
    public TextMeshProUGUI ourResources;

    public TextMeshProUGUI raca;
    public TextMeshProUGUI strenght;
    public TextMeshProUGUI attitude;
    public TextMeshProUGUI edatEspecie;
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
        gameManager.decisionMade = false;
        Background.SetTrigger("changeBG");
        if (startRound)
        {
            boleta.transform.position = boletaPlaceholderStart.transform.position;
            currentTime = 0;
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
            moonPhase = new float[planet.llunes.Length];
            for (int i = 0; i < moons.Length; i++)
            {
                moons[i] = Instantiate(planet.llunes[i], parent);
                moons[i].transform.SetSiblingIndex(siblingIndex+1);
                moonPhase[i] = Random.Range(0,360);
            }
        }

        planeta = planet;

        imatge.sprite = gameManager.factions[planeta.idFaction].imatge;

        ShowInfo(nom, planeta.Nom);
        ShowInfo(poblacio, TransformLong(planeta.Poblacio));

        string materialsString = "";
        for (int i = 0; i < gameManager.terra.materials.Length; i++)
        {
            if (i != 0) materialsString += " / "; 
            materialsString += TransformLong(gameManager.terra.materials[i]) ;
        }
        ShowInfo(ourResources, materialsString); 

        materialsString = "";
        for (int i = 0; i < planeta.materials.Length; i++)
        {
            if (i != 0) materialsString += " / ";
            materialsString += TransformLong(planeta.materials[i]);
        }
        ShowInfo(materialEnemic, materialsString);
        
        ShowInfo(tipus, planeta.tipusPlaneta.ToString());
        ShowInfo(perillositat, TransformDanger(planeta.perillositat));

        ShowInfo(raca, planeta.faction.especie.ToString());
        ShowInfo(strenght, string.Format("{0}%", planeta.faction.densitat));
        ShowInfo(attitude, TransformAttitude(planeta.faction.mitjaPerillositat));
        ShowInfo(edatEspecie, TransformAge(planeta.EdatEspecie));
        ShowInfo(agresivitat, TransformAgressive(planeta.faction.agresivitat));

        ShowInfo(day, string.Format("Day {0}", gameManager.round));
        ShowInfo(planetnumber, string.Format("Planet {0} of {1}", gameManager.roundCounter+1,gameManager.numPlanets));

        showData = true;
    }

    private string TransformAgressive(int agresivitat)
    {
        if (agresivitat < -60)
        {
            return "Very Friendly";
        }
        else if (agresivitat < -25)
        {
            return "Friendly";
        }
        else if (agresivitat < 25)
        {
            return "Neutral";
        }
        else if (agresivitat < 60)
        {
            return "Hostile";
        }
        else
        {
            return "Very Hostile";
        }
    }

    private string TransformAge(int edatEspecie)
    {
        if (edatEspecie > 260000)
        {
            return "Ancient";
        }
        else if (edatEspecie > 160000)
        {
            return "Matured";
        }
        else if (edatEspecie > 60000)
        {
            return "Recent";
        }
        else
        {
            return "Newborn";
        }
    }

    private string TransformAttitude(int mitjaPerillositat)
    {
        if (mitjaPerillositat < 30)
        {
            return "Passive";
        }
        else if (mitjaPerillositat < 60)
        {
            return "Neutral";
        }
        else
        {
            return "Dangerous";
        }
    }

    private string TransformDanger(int perillositat)
    {
        if (perillositat < 20)
        {
            return "Very Low";
        } else if (perillositat < 40)
        {
            return "Low";
        } else if (perillositat < 60)
        {
            return "Medium";
        } else if (perillositat < 80)
        {
            return "High";
        } else
        {
            return "Very Hign";
        }
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
        string retorn;
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

            if (currentTime > TimePerRound)
            {
                if (gameManager != null)
                    StartCoroutine(gameManager.NextPlanet());
                else
                    Debug.LogError("Game manager is null.");
            }

        }
        currentTime += Time.deltaTime;

        boleta.transform.position = Vector3.Lerp(boletaPlaceholderStart.transform.position, boletaPlaceholderEnd.transform.position, currentTime / TimePerRound);
        boleta.color = new Color(boleta.color.r, boleta.color.g, boleta.color.b, currentTime % 1);

        PlanetGO.transform.position = Vector3.Lerp(PlanetPlaceholderA.transform.position, PlanetPlaceholderB.transform.position, currentTime / TimePerRound);


        float orbit = 200f;
        for (int i = 0; i < moons.Length; i++)
        {
            float angle = 360f * ((float)i / (float)moons.Length);
            angle += currentTime * Mathf.Lerp(0.3f, 0.05f, ((float)i / (float)moons.Length));
            angle += moonPhase[i];//semi-randomnessssss
            moons[i].transform.position = PlanetGO.transform.position + new Vector3(orbit * Mathf.Sin(angle), orbit * Mathf.Cos(angle), 0);
            orbit += 55f;
        }
    }
}
