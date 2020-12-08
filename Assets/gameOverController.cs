using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverController : MonoBehaviour
{
    public TextMeshProUGUI[] texts;
    public string[] rawTexts;
    public float timePerChar = 0.1f;
    float currentTime = 0;
    float phaseTime = 0;
    int currentText = 0;
    int currentLeterIndex = 0;
    bool completed = true;

    public GameObject terraGO;

    // Start is called before the first frame update
    void Start()
    {
        StopAllCoroutines();

        //rawTexts = new string[texts.Length];
        for (int i = 0; i < texts.Length; i++)
        {
            //rawTexts[i] = texts[i].text;
            texts[i].text = "";
        }
        currentTime = 0;
        currentText = 0;
        currentLeterIndex = 0;
        StartCoroutine(StartPrint());
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        for (int i = 0; i < texts.Length; i++)
        {
            //rawTexts[i] = texts[i].text;
            texts[i].text = "";
        }
        currentTime = 0;
        currentText = 0;
        currentLeterIndex = 0;
        StartCoroutine(StartPrint());


    }

    IEnumerator StartPrint()
    {
        yield return new WaitForSeconds(2);
        completed = false;
    }

    // Update is called once per frame
    void Update()
    {
        phaseTime += Time.deltaTime;
        terraGO.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 1), Time.deltaTime * 5f);


        if (phaseTime > 15)
        {

            SceneManager.LoadScene("mainMenu");
        }

        if (completed)
        {
            return;
        }
        
        currentTime += Time.deltaTime;
        if (currentTime > timePerChar)
        {
            currentTime %= timePerChar;
            addLetter();  
        }


    }

    void addLetter() { 


        timePerChar = Random.Range(0.07f, 0.15f);
        if (currentLeterIndex >= rawTexts[currentText].Length)
        {
            currentText++;
            currentLeterIndex = 0;
            timePerChar = 0.5f;
            currentTime = 0;
        }
        
        if (currentText >= rawTexts.Length)
        {
            completed = true;
            return;

        }
        texts[currentText].text += rawTexts[currentText][currentLeterIndex];
        currentLeterIndex++;
    }
}
