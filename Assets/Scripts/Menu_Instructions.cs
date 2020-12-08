using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Menu_Instructions : MonoBehaviour
{

    public int current_page;

    public GameObject[] options;

    public Menu m;

    public Animator killPlanetButton;

    bool isButtonPress = false;

    void Start()
    {
        current_page = 0;
        options[0].gameObject.SetActive(true);
        options[1].gameObject.SetActive(false);
        options[2].gameObject.SetActive(false);
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
            
        }
    }



    public void activaFons()
    {
        options[3].transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(23f / 255f, 110f / 255f, 50f / 255f);
        options[3].gameObject.SetActive(true);
        options[3].transform.GetChild(0).gameObject.SetActive(false);
        options[4].gameObject.SetActive(false);
        options[4].transform.GetChild(0).gameObject.SetActive(false);

    }

    public IEnumerator Select()
    {
        if (isButtonPress)
        {

        }
        else
        {
            isButtonPress = true;
            switch (current_page)
            {
                // Cas 0 
                case 0:
                    killPlanetButton.SetBool("buttonDown", true);

                    options[0].gameObject.SetActive(false);
                    options[1].gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.2f);
                    killPlanetButton.SetBool("buttonDown", false);
                    isButtonPress = false;
                    current_page++;
                    break;
                case 1:
                    killPlanetButton.SetBool("buttonDown", true);
                    options[1].gameObject.SetActive(false);
                    options[2].gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.2f);
                    killPlanetButton.SetBool("buttonDown", false);
                    isButtonPress = false;
                    options[3].gameObject.SetActive(false);
                    options[4].gameObject.SetActive(true);
                    options[4].transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(23f / 255f, 110f / 255f, 50f / 255f);
                    current_page++;
                    break;
                case 2:
                    killPlanetButton.SetBool("buttonDown", true);
                    yield return new WaitForSeconds(0.2f);
                    killPlanetButton.SetBool("buttonDown", false);
                    isButtonPress = false;
                    current_page = 0;
                    options[2].gameObject.SetActive(false);
                    options[3].gameObject.SetActive(true);
                    options[4].gameObject.SetActive(false);
                    m.show_start();
                    options[0].gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        yield return null;
    }
    

}
