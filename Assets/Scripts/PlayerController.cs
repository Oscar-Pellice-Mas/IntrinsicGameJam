using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.roundActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameManager.NextPlanet();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameManager.DestroyPlanet();
            }
        }
        
    }
}
