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
            if (Input.GetButtonDown("A"))
            {
                gameManager.NextPlanet();
            }
            if (Input.GetButtonDown("B"))
            {
                gameManager.DestroyPlanet();
            }
        }
        
    }
}
