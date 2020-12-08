using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Menu_Opcions : MonoBehaviour
{

    public AudioMixer audioMixer;
    public SoundManager soundManager;

    public int current_option;

    public GameObject[] options;

    public Menu m;

    public Animator saveLeverAnimator;
    public Animator killPlanetButton;



    bool isLeverPress = false;
    bool isButtonPress = false;

    void Start()
    {
        activaFons();
    }

    private void Update()
    {
        if (Input.GetButtonDown("B"))
        {
            StartCoroutine(Select());
        }
        if (Input.GetButtonDown("A"))
        {
            StartCoroutine(Move());
        }
    }

    public IEnumerator Move()
    {
        if (isLeverPress)
        {

        }
        else
        {
            if (current_option < 2)
            {
                saveLeverAnimator.SetBool("palancaDown", true);
                current_option++;
            }
            else
            {
                saveLeverAnimator.SetBool("palancaDown", true);
                current_option = 0;
            }
            activaFons();
            yield return new WaitForSeconds(0.2f);
            saveLeverAnimator.SetBool("palancaDown", false);
            isLeverPress = false;
        }
            
        yield return null;
    }

    public void activaFons()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == current_option)
            {
                options[i].transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(23f / 255f, 110f / 255f, 50f / 255f);
                options[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                options[i].transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1);
                options[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator Select()
    {
        if (isButtonPress)
        {

        }
        else
        {
            isButtonPress = true;
            switch (current_option)
            {
                case 0:
                    killPlanetButton.SetBool("buttonDown", true);
                    if (options[0].transform.GetChild(2).GetComponent<Toggle>().isOn)
                    {
                        //killPlanetButton.SetBool("buttonDown", true);
                        //yield return new WaitForSeconds(0.2f);
                        //killPlanetButton.SetBool("buttonDown", false);
                        //isButtonPress = false;
                        options[0].transform.GetChild(2).GetComponent<Toggle>().isOn = false;
                        audioMixer.SetFloat("buttons", -80);
                        PlayerPrefs.SetInt("buttons", 0);
                    }
                    else
                    {
                        options[0].transform.GetChild(2).GetComponent<Toggle>().isOn = true;
                        audioMixer.SetFloat("buttons", -5);
                        soundManager.PlayButton();
                        PlayerPrefs.SetInt("buttons", 1);
                    }
                    yield return new WaitForSeconds(0.2f);
                    killPlanetButton.SetBool("buttonDown", false);
                    isButtonPress = false;
                    break;
                case 1:
                    killPlanetButton.SetBool("buttonDown", true);
                    if (options[1].transform.GetChild(2).GetComponent<Toggle>().isOn)
                    {
                        options[1].transform.GetChild(2).GetComponent<Toggle>().isOn = false;
                        audioMixer.SetFloat("music", -80);
                    }
                    else
                    {
                        options[1].transform.GetChild(2).GetComponent<Toggle>().isOn = true;
                        audioMixer.SetFloat("music", -5);
                    }
                    yield return new WaitForSeconds(0.2f);
                    killPlanetButton.SetBool("buttonDown", false);
                    isButtonPress = false;
                    break;
                case 2:
                    killPlanetButton.SetBool("buttonDown", true);
                    yield return new WaitForSeconds(0.2f);
                    killPlanetButton.SetBool("buttonDown", false);
                    isButtonPress = false;
                    m.show_start();
                    break;
                default:
                    break;
            }
        }
           
        yield return null;
    }
    

}
