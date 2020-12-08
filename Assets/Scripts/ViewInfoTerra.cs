using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewInfoTerra : MonoBehaviour
{
    public GameManager gameManager;

    //UI HOME
    public GameObject planetaHome;
    public GameObject TerraPlaceholder;

    public TextMeshProUGUI nomTerra;
    public TextMeshProUGUI poblacioTerra;
    public TextMeshProUGUI poblacioTerraVariacio;

    public TextMeshProUGUI recursosXTerra;
    public TextMeshProUGUI recursosCanviXTerra;
    public Image signeRecursosCanviXTerra;
    public TextMeshProUGUI consumXTerra;
    public TextMeshProUGUI recursosYTerra;
    public TextMeshProUGUI recursosCanviYTerra;
    public Image signeRecursosCanviYTerra;
    public TextMeshProUGUI consumYTerra;
    public TextMeshProUGUI recursosZTerra;
    public TextMeshProUGUI recursosCanviZTerra;
    public Image signeRecursosCanviZTerra;
    public TextMeshProUGUI consumZTerra;

    //public TextMeshProUGUI FaccioTerra;
    //public Image imatgeFaccioTerra;
    public GameObject Targeta1;
    public TextMeshProUGUI Faccio1;
    public TextMeshProUGUI dany1;
    public Image imatgeFaccio1;
    public GameObject Targeta2;
    public TextMeshProUGUI Faccio2;
    public TextMeshProUGUI dany2;
    public Image imatgeFaccio2;
    public GameObject Targeta3;
    public TextMeshProUGUI Faccio3;
    public TextMeshProUGUI dany3;
    public Image imatgeFaccio3;
    public GameObject Targeta4;
    public TextMeshProUGUI Faccio4;
    public TextMeshProUGUI dany4;
    public Image imatgeFaccio4;

    public bool GameOver = false;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetDataTerra(Terra terra, Terra terraAnterior)
    {
        long variacio;
        Destroy(planetaHome);
        planetaHome = Instantiate(terra.planetPrefab, TerraPlaceholder.transform);

        ShowInfo(nomTerra, terra.Nom);
        ShowInfo(poblacioTerra, string.Format("{0:n0}", terra.Poblacio));
        variacio = terra.Poblacio - terraAnterior.Poblacio;
        ColorOnValue(poblacioTerraVariacio, null, variacio);
        ShowInfo(poblacioTerraVariacio, TransformLong(variacio));

        ShowInfo(recursosXTerra, TransformLong(terra.materials[0]));
        variacio = terra.materials[0] - terraAnterior.materials[0];
        ColorOnValue(recursosCanviXTerra, signeRecursosCanviXTerra, variacio);
        ShowInfo(recursosCanviXTerra, TransformLong(variacio));
        ShowInfo(consumXTerra, string.Format("{0} / DAY", TransformLong(terra.consum[0])));

        variacio = terra.materials[1] - terraAnterior.materials[1];
        ShowInfo(recursosYTerra, TransformLong(terra.materials[1])); 
        ColorOnValue(recursosCanviYTerra, signeRecursosCanviYTerra, variacio);
        ShowInfo(recursosCanviYTerra, TransformLong(variacio));
        ShowInfo(consumYTerra, string.Format("{0} / DAY", TransformLong(terra.consum[1])));

        variacio = terra.materials[2] - terraAnterior.materials[2];
        ShowInfo(recursosZTerra, TransformLong(terra.materials[2]));
        ColorOnValue(recursosCanviZTerra, signeRecursosCanviZTerra, variacio);
        ShowInfo(recursosCanviZTerra, TransformLong(variacio));
        ShowInfo(consumZTerra, string.Format("{0} / DAY", TransformLong(terra.consum[2])));

        //ShowInfo(FaccioTerra, terra.faction.especie.ToString());
        //imatgeFaccioTerra = gameManager.factions[terra.idFaction].imatge;

        //Desactivar tots el contenidors d'atacs
        Targeta1.SetActive(false);
        Targeta2.SetActive(false);
        Targeta3.SetActive(false);
        Targeta4.SetActive(false);

        for (int i = 0; i < terra.atacants.Count; i++)
        {
            if (i == 0)
            {
                Targeta1.SetActive(true);
                imatgeFaccio1.sprite = terra.atacants[i].imatge;
                ShowInfo(Faccio1, terra.atacants[i].especie.ToString());
                ShowInfo(dany1, TransformLong(terra.danyAtac[i]));
            }
            if (i == 1)
            {
                Targeta2.SetActive(true);
                imatgeFaccio2.sprite = terra.atacants[i].imatge;
                ShowInfo(Faccio2, terra.atacants[i].especie.ToString());
                ShowInfo(dany2, TransformLong(terra.danyAtac[i]));
            }
            if (i == 2)
            {
                Targeta3.SetActive(true);
                imatgeFaccio3.sprite = terra.atacants[i].imatge;
                ShowInfo(Faccio3, terra.atacants[i].especie.ToString());
                ShowInfo(dany3, TransformLong(terra.danyAtac[i]));
            }
            if (i == 3)
            {
                Targeta4.SetActive(true);
                imatgeFaccio4.sprite = terra.atacants[i].imatge;
                ShowInfo(Faccio4, terra.atacants[i].especie.ToString());
                ShowInfo(dany4, TransformLong(terra.danyAtac[i]));
            }
        }

        if (terra.Poblacio <= 0)
        {
            gameManager.GameOver = true;


        }
    }

    IEnumerator showGameOverScreen()
    {

        //Debug.Log("GAME OVER!");

        yield return null;
    }

    private void ColorOnValue(TextMeshProUGUI component, Image image, long value)
    {
        if (value == 0)
        {
            component.color = Color.black;
            if(image != null)
            {
                image.color = Color.black;
                image.transform.localScale.Set(1, 1, 1);
            }
            
        }
        else if (value < 0)
        {
            component.color = Color.red;
            if (image != null)
            {
                image.color = Color.red;
                //image.transform.localScale = new Vector3(1, -1, 1);
                image.transform.rotation = Quaternion.Euler(0, 0, -180);
            }
        } else
        {
            component.color = Color.green;
            if (image != null)
            {
                image.color = Color.green;
                image.transform.rotation = Quaternion.Euler(0, 0, 0);
                //image.transform.localScale.Set(1, 1, 1);
            }
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
        if (data < 0)
        {
            retorn = string.Format("-{0}", TransformInt(-data));
        }
        else if (data / 1000 < 1)
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

    private string TransformLong(long data)
    {
        string retorn;

        if (data < 0)
        {
            retorn = string.Format("-{0}", TransformLong(-data));
        }
        else if (data / 1000 < 1)
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

}

