using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource button;
    public AudioSource explosio;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        button.Play();
    }
    public void PlayDestruction()
    {
        explosio.Play();
    }
}
