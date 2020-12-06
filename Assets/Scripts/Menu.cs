using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject start_menu;
    public GameObject options_menu;
    public GameObject credits_menu;

    public int selected_Option;
    public GameObject[] textos;

    public Animator saveLeverAnimator;
    public Animator killPlanetButton;

    bool isLeverPress = false;
    bool isButtonPress = false;
    void Start()
    {
        initGame();

    }

    private void Update()
    {
        if (Input.GetButtonDown("A"))
        {
            StartCoroutine(Select());
        }
        if (Input.GetButtonDown("B"))
        {
            StartCoroutine(Move());
        }
    }

    public void initGame()
    {
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

    public void show_options()
    {
        start_menu.gameObject.SetActive(false);
        options_menu.gameObject.SetActive(true);
        credits_menu.gameObject.SetActive(false);
    }

    public void show_start()
    {
        isLeverPress = false;
        isButtonPress = false;
        start_menu.gameObject.SetActive(true);
        options_menu.gameObject.SetActive(false);
        credits_menu.gameObject.SetActive(false);
    }

    public void startGame()
    {
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
            if (selected_Option < 2)
            {
                saveLeverAnimator.SetBool("palancaDown", true);
                selected_Option++;
            }
            else
            {
                saveLeverAnimator.SetBool("palancaDown", true);
                selected_Option = 0;
            }
            for (int i = 0; i < 3; i++)
            {
                if (i == selected_Option)
                {
                    textos[i].GetComponent<TMPro.TextMeshProUGUI>().color = new Color(23f / 255f, 110f / 255f, 50f / 255f);
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
            Debug.Log("Entra a select");
            isButtonPress = true;
            switch (selected_Option)
            {
                case 0:
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
                    killPlanetButton.SetBool("buttonDown", true);
                    yield return new WaitForSeconds(0.2f);
                    killPlanetButton.SetBool("buttonDown", false);
                    isButtonPress = false;
                    show_options();
                    break;
                case 2:
                    // Aixo suposo q són els credits
                    killPlanetButton.SetBool("buttonDown", true);
                    yield return new WaitForSeconds(0.2f);
                    killPlanetButton.SetBool("buttonDown", false);
                    isButtonPress = false;
                    break;
                default:
                    break;
            }
        }
            
        yield return null;
    }


}
