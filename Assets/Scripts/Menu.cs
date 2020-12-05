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
    void Start()
    {
        initGame();

    }

    private void Update()
    {
        if (Input.GetButtonDown("Seleccionar"))
        {
            Select();
        }
        if (Input.GetButtonDown("Moure"))
        {
            Move();
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
                textos[i].GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255, 0, 0);
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
        start_menu.gameObject.SetActive(true);
        options_menu.gameObject.SetActive(false);
        credits_menu.gameObject.SetActive(false);
    }

    public void startGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("mainGame");

    }

    public void Move()
    {

        if (selected_Option < 2)
        {
            selected_Option++;
        }
        else
        {
            selected_Option = 0;
        }
        for (int i = 0; i < 3; i++)
        {
            if (i == selected_Option)
            {
                textos[i].GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255, 0, 0);
            }
            else
            {
                textos[i].GetComponent<TMPro.TextMeshProUGUI>().color = new Color(10, 10, 10);
            }
        }
    }


    public void Select()
    {
        switch (selected_Option)
        {
            case 0:
                startGame();
                break;
            case 1:
                show_options();
                break;
            case 2:
                break;
            default:
                break;
        }
    }


}
