﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewInfoTerra : MonoBehaviour
{
    public GameManager gameManager;

    //UI HOME
    //public GameObject planetaHome;

    public TextMeshProUGUI nomTerra;
    public TextMeshProUGUI poblacioTerra;
    public TextMeshProUGUI poblacioTerraVariacio;

    public TextMeshProUGUI recursosXTerra;
    public TextMeshProUGUI recursosCanviXTerra;
    //public TextMeshProUGUI consumXTerra;
    public TextMeshProUGUI recursosYTerra;
    public TextMeshProUGUI recursosCanviYTerra;
    //public TextMeshProUGUI consumYTerra;
    public TextMeshProUGUI recursosZTerra;
    public TextMeshProUGUI recursosCanviZTerra;
    //public TextMeshProUGUI consumZTerra;

    //public TextMeshProUGUI FaccioTerra;
    //public Sprite imatgeFaccioTerra;
    public TextMeshProUGUI Faccio1;
    public TextMeshProUGUI dany1;
    //public Sprite imatgeFaccio1;
    public TextMeshProUGUI Faccio2;
    public TextMeshProUGUI dany2;
    //public Sprite imatgeFaccio2;
    public TextMeshProUGUI Faccio3;
    public TextMeshProUGUI dany3;
    //public Sprite imatgeFaccio3;
    public TextMeshProUGUI Faccio4;
    public TextMeshProUGUI dany4;
    //public Sprite imatgeFaccio4;


    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetDataTerra(Terra terra, Terra terraAnterior)
    {
        //Transform parent = planetaHome.transform.parent;
        //int siblingIndex = planetaHome.transform.GetSiblingIndex();
        //DestroyImmediate(planetaHome);

        //planetaHome = Instantiate(terra.planetPrefab, parent);
        //planetaHome.transform.SetSiblingIndex(siblingIndex);
        ShowInfo(nomTerra, terra.Nom);
        ShowInfo(poblacioTerra, TransformLong(terra.Poblacio));
        ShowInfo(poblacioTerraVariacio, TransformLong(terra.Poblacio-terraAnterior.Poblacio));

        ShowInfo(recursosXTerra, terra.materials[0].ToString());
        Debug.Log(terra.materials[0]);
        Debug.Log(terraAnterior.materials[0]);
        int variacio = terra.materials[0] - terraAnterior.materials[0];
        ShowInfo(recursosCanviXTerra, variacio.ToString());
        //ShowInfo(consumXTerra, terra.consum[0].ToString());

        variacio = terra.materials[1] - terraAnterior.materials[1];
        ShowInfo(recursosYTerra, terra.materials[1].ToString());
        ShowInfo(recursosCanviYTerra, variacio.ToString());
        //ShowInfo(consumYTerra, terra.consum[1].ToString());

        variacio = terra.materials[2] - terraAnterior.materials[2];
        ShowInfo(recursosZTerra, terra.materials[2].ToString());
        ShowInfo(recursosCanviZTerra, variacio.ToString());
        //ShowInfo(consumZTerra, terra.consum[2].ToString());

        //ShowInfo(FaccioTerra, terra.faction.especie.ToString());
        //imatgeFaccioTerra = gameManager.factions[terra.idFaction].imatge;

        //Desactivar tots el contenidors d'atacs

        for (int i = 0; i < terra.atacants.Count; i++)
        {
            if (i == 0)
            {
                //Activar contenidor d'atac
                //imatgeFaccio1 = terra.atacants[i].imatge;
                ShowInfo(Faccio1, terra.atacants[i].especie.ToString());
                ShowInfo(dany1, TransformLong(terra.danyAtac[i]));
            }
            if (i == 1)
            {
                //Activar contenidor d'atac
                //imatgeFaccio2 = terra.atacants[i].imatge;
                ShowInfo(Faccio2, terra.atacants[i].especie.ToString());
                ShowInfo(dany2, TransformLong(terra.danyAtac[i]));
            }
            if (i == 2)
            {
                //Activar contenidor d'atac
                //imatgeFaccio3 = terra.atacants[i].imatge;
                ShowInfo(Faccio3, terra.atacants[i].especie.ToString());
                ShowInfo(dany3, TransformLong(terra.danyAtac[i]));
            }
            if (i == 3)
            {
                //Activar contenidor d'atac
                //imatgeFaccio4 = terra.atacants[i].imatge;
                ShowInfo(Faccio4, terra.atacants[i].especie.ToString());
                ShowInfo(dany4, TransformLong(terra.danyAtac[i]));
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

}
