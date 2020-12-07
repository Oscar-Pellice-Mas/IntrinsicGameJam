using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Menu_Instructions : MonoBehaviour
{
    public GameObject[] options;

    public Menu m;

    public Animator killPlanetButton;

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
            
        }
    }



    public void activaFons()
    {

        options[0].transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(23f / 255f, 110f / 255f, 50f / 255f);
        options[0].transform.GetChild(0).gameObject.SetActive(false);

    }

    public IEnumerator Select()
    {
        if (isButtonPress)
        {

        }
        else
        {
            isButtonPress = true;
            killPlanetButton.SetBool("buttonDown", true);
            yield return new WaitForSeconds(0.2f);
            killPlanetButton.SetBool("buttonDown", false);
            isButtonPress = false;
            m.show_start();
        }
           
        yield return null;
    }
    

}
