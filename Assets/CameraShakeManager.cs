using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public Transform[] ObjectsToShake;
    Vector3[] InitialPositions;
    public bool triggerShake = false;
    bool isShaking = false;
    float shakeDuration = 1;
    float currentShakeDuration = 0;
    public float ShakeAmplitude = 5;
    void Start()
    {
        InitialPositions = new Vector3[ObjectsToShake.Length];
        for (int i = 0; i < ObjectsToShake.Length; i++)
        {
            InitialPositions[i] = ObjectsToShake[i].transform.position;
        }
    }

    public void StartShake()
    {
        triggerShake = true;
    }

    void Update()
    {
        if (triggerShake)
        {
            triggerShake = false;

            isShaking = true;
            currentShakeDuration = shakeDuration;
        }

        if (isShaking)
        {
            float scale = ShakeAmplitude * (1-((-currentShakeDuration+shakeDuration) / shakeDuration));
            Vector3 offset = new Vector3(Random.Range(-scale, scale), Random.Range(-scale, scale), 0);

            for (int i = 0; i < ObjectsToShake.Length; i++)
            {

                ObjectsToShake[i].transform.position = InitialPositions[i] + offset;
            }

            currentShakeDuration -= Time.deltaTime;
            if (currentShakeDuration <= 0)
            {
                isShaking = false;
            }
        }
    }
}
