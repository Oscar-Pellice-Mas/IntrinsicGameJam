﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject start_menu;
    public GameObject options_menu;
    public GameObject howtoplay_menu;
    public GameObject credits_menu;

    public int selected_Option;
    public GameObject[] textos;

    public GameObject menu_panel;
    public GameObject menu_controls;
    public GameObject palanca;
    public GameObject boto;

    public Animator saveLeverAnimator;
    public Animator killPlanetButton;

    public ScrollZ sz;

    bool isLeverPress = false;
    bool isButtonPress = false;

    bool menuActive = false;

    

    void Start()
    {
        initGame();

    }

    private void Update()
    {
        if (Input.GetButtonDown("A"))
        {
            StartCoroutine(Select());
            options_menu.GetComponent<Menu_Opcions>().soundManager.PlayButton();
        }
        if (Input.GetButtonDown("B"))
        {
            StartCoroutine(Move());
            options_menu.GetComponent<Menu_Opcions>().soundManager.PlayButton();
        }
    }

    public void initGame()
    {
        menuActive = true;
        start_menu.gameObject.SetActive(true);
        options_menu.gameObject.SetActive(false);
        credits_menu.gameObject.SetActive(false);
        selected_Option = 0;

        for (int i = 0; i < 3; i++)
        {
            if (i == selected_Option)
            {
                textos[i].GetComponent<TMPro.TextMeshProUGUI>().color = new Color(23f/255f, 110f/255f, 50f/255f);
            }
            else
            {
                textos[i].GetComponent<TMPro.TextMeshProUGUI>().color = new Color(10, 10, 10);
            }
        }
    }

    public void show_credits()
    {
        Debug.Log("Show Credits");
        menuActive = false;
        isLeverPress = false;
        isButtonPress = false;
        start_menu.gameObject.SetActive(false);
        options_menu.gameObject.SetActive(false);
        howtoplay_menu.gameObject.SetActive(false);
        credits_menu.gameObject.SetActive(true);

        menu_controls.gameObject.SetActive(false);
        menu_panel.gameObject.SetActive(false);
        palanca.gameObject.SetActive(false);
        boto.gameObject.SetActive(false);

        sz.activarScroll = true;

    }

    public void show_howtoplay()
    {
        Debug.Log("Show How To Play");
        menuActive = false;
        isLeverPress = false;
        isButtonPress = false;
        start_menu.gameObject.SetActive(false);
        options_menu.gameObject.SetActive(false);
        howtoplay_menu.gameObject.SetActive(true);
        credits_menu.gameObject.SetActive(false);

    }

    public void show_options()
    {
        Debug.Log("Show Options");
        menuActive = false;
        isLeverPress = false;
        isButtonPress = false;
        start_menu.gameObject.SetActive(false);
        options_menu.gameObject.SetActive(true);
        howtoplay_menu.gameObject.SetActive(false);
        credits_menu.gameObject.SetActive(false);
    }

    public void show_start()
    {
        Debug.Log("Show Start");
        menuActive = true;
        isLeverPress = false;
        isButtonPress = false;
        start_menu.gameObject.SetActive(true);
        options_menu.gameObject.SetActive(false);
        howtoplay_menu.gameObject.SetActive(false);
        credits_menu.gameObject.SetActive(false);

        menu_controls.gameObject.SetActive(true);
        menu_panel.gameObject.SetActive(true);
        palanca.gameObject.SetActive(true);
        boto.gameObject.SetActive(true);
    }

    public void startGame()
    {
        menuActive = false;
        Debug.Log("Start Game");
        SceneManager.LoadScene("mainGame");

    }

    public IEnumerator Move()
    {
        if (isLeverPress)
        {
            
        } else
        {
            isLeverPress = true;
            if (selected_Option < 3)
            {
                saveLeverAnimator.SetBool("palancaDown", true);
                selected_Option++;
            }
            else
            {
                saveLeverAnimator.SetBool("palancaDown", true);
                selected_Option = 0;
            }
            for (int i = 0; i < 4; i++)
            {
                if (i == selected_Option)
                {
                    textos[i].GetComponent<TMPro.TextMeshProUGUI>().color = new Color(23f / 255f, 110f / 255f, 50f / 255f);
                }
                else
                {
                    textos[i].GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1);
                }
            }
            yield return new WaitForSeconds(0.2f);
            saveLeverAnimator.SetBool("palancaDown", false);
            isLeverPress = false;
        }
        
        yield return null;
    }


    public IEnumerator Select()
    {
        if (isButtonPress)
        {

        }
        else
        {
            if (menuActive)
            {
                isButtonPress = true;
                switch (selected_Option)
                {
                    case 0:
                        selected_Option = 0;
                        if (start_menu.activeSelf)
                        {
                            killPlanetButton.SetBool("buttonDown", true);
                            yield return new WaitForSeconds(0.2f);
                            killPlanetButton.SetBool("buttonDown", false);
                            isButtonPress = false;
                            startGame();
                        }

                        break;
                    case 1:
                        selected_Option = 0;
                        killPlanetButton.SetBool("buttonDown", true);
                        yield return new WaitForSeconds(0.2f);
                        killPlanetButton.SetBool("buttonDown", false);
                        isButtonPress = false;
                        show_options();
                        break;
                    case 2:
                        selected_Option = 0;
                        show_howtoplay();
                        killPlanetButton.SetBool("buttonDown", true);
                        yield return new WaitForSeconds(0.2f);
                        killPlanetButton.SetBool("buttonDown", false);
                        isButtonPress = false;
                        break;
                    case 3:
                        selected_Option = 0;
                        show_credits();
                        killPlanetButton.SetBool("buttonDown", true);
                        yield return new WaitForSeconds(0.2f);
                        killPlanetButton.SetBool("buttonDown", false);
                        isButtonPress = false;
                        break;
                    default:
                        selected_Option = 0;
                        break;
                }
            }
            
        }
            
        yield return null;
    }


}
