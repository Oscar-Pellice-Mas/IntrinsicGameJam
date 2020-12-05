using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Menu_Opcions : MonoBehaviour
{

    public AudioMixer audioMixer;

    public int current_option;

    public GameObject[] options;

    public Menu m;

    void Start()
    {
        activaFons();
    }

    private void Update()
    {
        if (Input.GetButtonDown("A"))
        {
            Select();
        }
        if (Input.GetButtonDown("B"))
        {
            Move();
        }
    }

    public void Move()
    {
        if (current_option < 2)
        {
            current_option++;
        }
        else
        {
            current_option = 0;
        }
        activaFons();
    }

    public void activaFons()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == current_option)
            {
                options[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                options[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void Select()
    {

        switch (current_option)
        {
            case 0:
                if (options[0].transform.GetChild(2).GetComponent<Toggle>().isOn)
                {
                    options[0].transform.GetChild(2).GetComponent<Toggle>().isOn = false;
                    audioMixer.SetFloat("sounds", 0);
                }
                else
                {
                    options[0].transform.GetChild(2).GetComponent<Toggle>().isOn = true;
                    audioMixer.SetFloat("sounds", 10);
                }
                break;
            case 1:
                if (options[1].transform.GetChild(2).GetComponent<Toggle>().isOn)
                {
                    options[1].transform.GetChild(2).GetComponent<Toggle>().isOn = false;
                    audioMixer.SetFloat("music", 0);
                }
                else
                {
                    options[1].transform.GetChild(2).GetComponent<Toggle>().isOn = true;
                    audioMixer.SetFloat("music", 10);
                }
                break;
            case 2:
                m.show_start();
                break;
            default:
                break;
        }
    }

}
