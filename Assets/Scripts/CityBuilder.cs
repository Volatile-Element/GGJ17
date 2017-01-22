using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CityBuilder
{
    public string ResourcesLocationForCity = "City";

    public GameObject BuildCity()
    {
        //TODO: Change the scale, colour, health, damage etc
        var city = Resources.LoadAll<GameObject>(ResourcesLocationForCity).FirstOrDefault() as GameObject;
        
        return city;
    }
}