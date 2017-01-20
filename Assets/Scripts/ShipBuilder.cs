using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipBuilder
{
    public string ResourcesLocationForParts = "Ship Parts";
    public string ResourcesLocationForShip = "Ship";

	public IEnumerable<GameObject> BuildShip()
    {
        var ship = Resources.LoadAll<GameObject>(ResourcesLocationForShip).FirstOrDefault();
        var possibleParts = Resources.LoadAll<GameObject>(ResourcesLocationForParts);
        
        if (ship == null)
        {
            Debug.LogError("No ship has been loaded. Check your paths please.");
            return Enumerable.Empty<GameObject>();
        }
        if (possibleParts == null)
        {
            Debug.LogError("No ship parts has been loaded. Check your paths please.");
            return Enumerable.Empty<GameObject>();
        }

        return new GameObject[]
        {
            ship,
            possibleParts[Random.Range(0, possibleParts.Length)]
        };
    }
}