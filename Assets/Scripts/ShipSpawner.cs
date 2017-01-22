﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Hello there. I manage the spawning and despawning of ships.
/// </summary>
public class ShipSpawner : MonoBehaviour
{
    public ShipBuilder ShipBuilder = new ShipBuilder();
    public List<GameObject> SpawnedShips = new List<GameObject>();

    public float MaxXSpawn = 100;
    public float MinXSpawn = 0;
    public float MaxYSpawn = 100;
    public float MinYSpawn = 0;

    public float MinSpawnTime = 1;
    public float MaxSpawntime = 1;

    void Start ()
    {
	}

    public GameObject SpawnShip()
    {
        var shipParts = ShipBuilder.BuildShip();
        var ship = shipParts.Take(1).FirstOrDefault();
        var parts = shipParts.Skip(1);

        var spawnPoint = GenerateSpawnPoint();

        var spawnedShip = Instantiate(ship, spawnPoint, Quaternion.identity) as GameObject;
        foreach (var part in parts)
        {
            var spawnedPart = Instantiate(part, spawnPoint, Quaternion.identity) as GameObject;
            spawnedPart.transform.parent = spawnedShip.transform;
        }

        spawnedShip = SetColour(spawnedShip);

        spawnedShip.transform.LookAt(new Vector3(0, PlaneManager.Instance.GetCurrentPlaneHeight(), 0));

        SpawnedShips.Add(spawnedShip);
        IncrementDifficulty();

        return spawnedShip;
    }

    private GameObject SetColour(GameObject city)
    {
        var color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        foreach (var child in city.GetComponentsInChildren<Renderer>())
        {
            foreach (var material in child.materials)
            {
                if (material.HasProperty("_Color"))
                {
                    material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), material.color.a);
                }
            }
        }

        return city;
    }

    private Vector3 GenerateSpawnPoint()
    {
        int angle = Random.Range(0, 360);

        var mineX = 1000 * Mathf.Cos(angle);
        var mineY = 1000 * Mathf.Sin(angle);

        return new Vector3(mineX, PlaneManager.Instance.GetCurrentPlaneHeight(), mineY);
    }

    private void IncrementDifficulty()
    {
        GameManager.Instance.DifficultyManager.IncrementSpawnedShips();

        MinSpawnTime = Mathf.Clamp(10 - GameManager.Instance.DifficultyManager.GetDifficultyMultiplier() * 2, 0.5f, 10);
        MaxSpawntime = Mathf.Clamp(10 - GameManager.Instance.DifficultyManager.GetDifficultyMultiplier(), 0.5f, 10);
    }

    public void DestoryShip(GameObject ship)
    {
        SpawnedShips.Remove(ship);

        Destroy(ship);

        GameManager.Instance.ScoreKeeper.AddToScore(Enums.ScoreReward.DESTROYED_SHIP);
    }
}