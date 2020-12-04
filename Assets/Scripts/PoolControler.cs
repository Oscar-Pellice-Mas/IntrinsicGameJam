using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControler : MonoBehaviour
{
    public static PoolControler SharedInstance;

    public List<Planet> planetObjects;
    public Planet objectToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    public void CreatePool (int num)
    {
        for (int i = 0; i < num; i++)
        {
            Planet obj = (Planet)Instantiate(objectToPool);
            PlanetGenerator.GeneratePlanet();
            planetObjects.Add(obj);
        }
    }

    public Planet GetPooledObject()
    {
        Planet obj = null;
        if (planetObjects.Count > 0)
        {
            obj = planetObjects[0];
            planetObjects.RemoveAt(0);
        }
        return obj;
    }
}
