using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PoolControler poolControler;
    
    public ViewInfoPlanet viewInfo;

    public bool roundActive = false;

    void Start()
    {
        poolControler = GetComponent<PoolControler>();
        poolControler.CreatePool(5);
        viewInfo.SetData(poolControler.GetPooledObject());
        roundActive = true;
    }

    public void NextPlanet()
    {
        viewInfo.SetData(poolControler.GetPooledObject());
    }

    public void DestroyPlanet()
    {
        viewInfo.SetData(poolControler.GetPooledObject());
        Debug.Log("DESTROYED");
    }

    public void RoundDone()
    {
        roundActive = false;
    }

}
