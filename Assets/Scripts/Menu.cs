using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject start_menu;
    public GameObject options_menu;
    public GameObject howtoplay_menu;
    public GameObject credits_menu;

    public int selected_Option;
    public GameObject[] textos;
    public Vector3[] initialPos;
    public Color selectedColor;
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
        initialPos = new Vector3[textos.Length];
        for (int i = 0; i < initialPos.Length; i++)
        {
            initialPos[i] = textos[i].GetComponent<RectTransform>().position;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("B"))
        {
            StartCoroutine(Select());
            options_menu.GetComponent<Menu_Opcions>().soundManager.PlayButton();
        }
        if (Input.GetButtonDown("A"))
        {
            StartCoroutine(Move());
            options_menu.GetComponent<Menu_Opcions>().soundManager.PlayButton();
        }

        for (int i = 0; i < textos.Length; i++)
        {
            textos[i].GetComponent<RectTransform>().position = Vector3.Lerp(textos[i].GetComponent<RectTransform>().position, initialPos[i], 0.03f);
        }

    }
    

    public void initGame()
    {
        menuActive = true;
        start_menu.gameObject.SetActive(true);
        options_menu.gameObject.SetActive(false);
        credits_menu.gameObject.SetActive(false);
        howtoplay_menu.gameObject.SetActive(false);
        isLeverPress = false;
        isButtonPress = false;

        menu_controls.gameObject.SetActive(true);
        menu_panel.gameObject.SetActive(true);
        palanca.gameObject.SetActive(true);
        boto.gameObject.SetActive(true);

        selected_Option = 0;

        for (int i = 0; i < 4; i++)
        {
            if (i == selected_Option)
            {
                textos[i].GetComponent<RectTransform>().position += Vector3.down * 7;
                textos[i].GetComponent<TMPro.TextMeshProUGUI>().color = selectedColor;
            }
            else
            {
                textos[i].GetComponent<TMPro.TextMeshProUGUI>().color = new Color(10, 10, 10);
            }
        }

    }

    public void show_start()
    {
        initGame();

    }

    public void show_credits()
    {
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
        menuActive = false;
        isLeverPress = false;
        isButtonPress = false;
        start_menu.gameObject.SetActive(false);
        options_menu.gameObject.SetActive(true);
        howtoplay_menu.gameObject.SetActive(false);
        credits_menu.gameObject.SetActive(false);
    }

   

    public void startGame()
    {
        menuActive = false;

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
                    textos[i].GetComponent<RectTransform>().position += Vector3.down * 7;
                    textos[i].GetComponent<TMPro.TextMeshProUGUI>().color = selectedColor;
                }
                else
                {
                    textos[i].GetComponent<TMPro.TextMeshProUGUI>().color = new Color(10, 10, 10);
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
