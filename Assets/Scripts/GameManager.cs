using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PoolControler poolControler;
    public GameObject prefabViewInfo;
    GameObject viewInfo;

    public bool roundActive = false;

    void Start()
    {
        poolControler = GetComponent<PoolControler>();
        poolControler.CreatePool(5);
        viewInfo = Instantiate(prefabViewInfo, Vector3.zero, Quaternion.identity);
        viewInfo.GetComponent<ViewInfoPlanet>().SetData(poolControler.GetPooledObject());
        roundActive = true;
    }

    public void NextPlanet()
    {
        viewInfo.GetComponent<ViewInfoPlanet>().SetData(poolControler.GetPooledObject());
    }

    public void DestroyPlanet()
    {
        viewInfo.GetComponent<ViewInfoPlanet>().SetData(poolControler.GetPooledObject());
        Debug.Log("DESTROYED");
    }

    public void RoundDone()
    {
        roundActive = false;
    }

}
