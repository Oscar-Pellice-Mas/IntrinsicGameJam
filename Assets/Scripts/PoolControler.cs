using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControler : MonoBehaviour
{
    private PoolControler SharedInstance;

    public List<Planet> planetObjects;
    public Planet objectToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    public void CreatePool (int num)
    {
        Planet planet;
        for (int i = 0; i < num; i++)
        {
            planet = PlanetGenerator.GeneratePlanet();
            planetObjects.Add(planet);
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
