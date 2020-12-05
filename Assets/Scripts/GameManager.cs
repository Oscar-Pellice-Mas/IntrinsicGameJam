using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PoolControler poolControler;
    public GameObject prefabViewInfo;

    // Start is called before the first frame update
    void Start()
    {
        poolControler = GetComponent<PoolControler>();
        poolControler.CreatePool(5);
        Instantiate(prefabViewInfo, Vector3.zero, Quaternion.identity);
        prefabViewInfo.GetComponent<ViewInfoPlanet>().SetData(poolControler.GetPooledObject());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
