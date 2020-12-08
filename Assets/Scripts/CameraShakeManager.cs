using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShakeManager : MonoBehaviour
{
    public Transform[] ObjectsToShake;
    public Image[] ObjectsToTint;
    Vector3[] InitialPositions;

    //shake
    [SerializeField]
    bool triggerShake = false;
    bool isShaking = false;
    float currentShakeDuration = 0;
    public float ShakeAmplitude = 5;
    public float shakeDuration = 1;
    public ShakeType shakeType;
    public enum ShakeType { incremental, decremental, constant}

    //Tint
    [SerializeField]
    bool trigerTint = false;
    bool isTinted = false;
    bool currentlyTinted = false;
    public float timeOn = 0.5f;
    public float timeOff = 1f;
    float partialtime = 0;
    float currentTintTime = 0;
    public int FlickerCount = 5;
    [HideInInspector]
    public float tintDuration;
    public Color tintedColor = Color.red;

    //blackscreen
    public Image BlackScreenCanvas;
    float BlackScreenProgress = 0;
    bool ShouldBeBlack = false;
    float currentBlackScreenProgress = 0;
    public float BlackscreenAnimationDuration = 1;
    void Awake()
    {
        InitialPositions = new Vector3[ObjectsToShake.Length];
        for (int i = 0; i < ObjectsToShake.Length; i++)
        {
            InitialPositions[i] = ObjectsToShake[i].transform.position;
        }
        currentBlackScreenProgress = BlackscreenAnimationDuration;
        BlackScreenCanvas.color = new Color(0, 0, 0, 1);
        ShouldBeBlack = false;
    }



    public void StartShake(float shakeduration, float amplitude, ShakeType shakeType_)
    {
        shakeType = shakeType_;
        shakeDuration = shakeduration;
        ShakeAmplitude = amplitude;
        triggerShake = true;
    }

    public void StartFlicker()
    {
        trigerTint = true;
    }

    public float GetTintDuration()
    {
        return FlickerCount * (timeOn + timeOff);
    }
    public void ShowBlackScreen()
    {
        ShouldBeBlack = true;
        currentBlackScreenProgress = Mathf.Clamp(currentBlackScreenProgress, 0, BlackscreenAnimationDuration);
    }

    public void HideBlackScreen()
    {
        ShouldBeBlack = false;
        currentBlackScreenProgress = Mathf.Clamp(currentBlackScreenProgress, 0, BlackscreenAnimationDuration);
    }
    void Update()
    {
        currentBlackScreenProgress = Mathf.Clamp(currentBlackScreenProgress, 0, BlackscreenAnimationDuration);
        BlackScreenCanvas.color = new Color(0,0,0, Mathf.Pow(currentBlackScreenProgress/ BlackscreenAnimationDuration, 2));
        if (ShouldBeBlack)
        {
            currentBlackScreenProgress += Time.deltaTime;
        }
        else
        {
            currentBlackScreenProgress -= Time.deltaTime;
        }

        if (trigerTint)
        {
            tintDuration = FlickerCount * (timeOn+ timeOff);
            isTinted = true;
            trigerTint = false;
            currentTintTime = tintDuration;
        }

        if (isTinted)
        {


            for (int i = 0; i < ObjectsToTint.Length; i++)
            {
                ObjectsToTint[i].color = Color.Lerp(ObjectsToTint[i].color, (currentlyTinted) ? tintedColor : Color.white, 0.02f);
            }

            partialtime += Time.deltaTime;

            if (currentlyTinted)
            {
                if (partialtime >= timeOn)
                {
                    partialtime = 0;
                    currentlyTinted = false;
                }
            }
            else
            {
                if (partialtime >= timeOff)
                {
                    partialtime = 0;
                    currentlyTinted = true;
                }
            }

            currentTintTime -= Time.deltaTime;
            if (currentTintTime < 0)
            {
                isTinted = false;
            }

        }
        else
        {
            for (int i = 0; i < ObjectsToTint.Length; i++)
            {
                ObjectsToTint[i].color = Color.Lerp(ObjectsToTint[i].color, Color.white, 0.02f);
            }
        }

        if (triggerShake)
        {
            triggerShake = false;

            isShaking = true;
            currentShakeDuration = shakeDuration;
        }

        if (isShaking)
        {
            float scale = 1;
            if (shakeType == ShakeType.decremental)
            {
                scale = ShakeAmplitude * (1 - ((-currentShakeDuration + shakeDuration) / shakeDuration));


            }else if (shakeType == ShakeType.incremental)
            {
                scale = ShakeAmplitude * (((-currentShakeDuration + shakeDuration) / shakeDuration));

            }
            Vector3 offset = new Vector3(Random.Range(-scale, scale), Random.Range(-scale, scale), 0);

            for (int i = 0; i < ObjectsToShake.Length; i++)
            {

                ObjectsToShake[i].transform.position = InitialPositions[i] + offset;
            }

            currentShakeDuration -= Time.deltaTime;

            if (currentShakeDuration <= 0)
            {
                isShaking = false;

                for (int i = 0; i < ObjectsToShake.Length; i++)
                {
                    ObjectsToShake[i].transform.position = InitialPositions[i];
                }
            }
        }
    }
}
