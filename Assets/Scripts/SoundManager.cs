using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource button;
    public AudioSource explosio;
    public AudioSource travel;
    public AudioSource laser;
    public AudioSource alarma;
    void Start()
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
    public void PlayAlarma()
    {
        alarma.Play();
    }
    public void PlayLaser()
    {
        laser.Play();
    }
    public void PlayTravel()
    {
        travel.Play();
    }
}
