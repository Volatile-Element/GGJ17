using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CitySpawner : MonoBehaviour
{
    public CityBuilder CityBuilder = new CityBuilder();
    public List<GameObject> SpawnedCities = new List<GameObject>();

    public float MaxXSpawn = 100;
    public float MinXSpawn = 0;
    public float MaxYSpawn = 100;
    public float MinYSpawn = 0;

    public float MinSpawnTime = 1;
    public float MaxSpawntime = 1;

    public UnityEvent OnCitySpawned = new UnityEvent();

    public GameObject SpawnCity()
    {
        var city = CityBuilder.BuildCity();

        var spawnPoint = GenerateSpawnPoint();

        var spawnedCity = Instantiate(city, spawnPoint, Quaternion.identity) as GameObject;
        spawnedCity = SetColour(spawnedCity);

        spawnedCity.transform.LookAt(new Vector3(0, PlaneManager.Instance.GetCurrentPlaneHeight(), 0));

        SpawnedCities.Add(spawnedCity);
        IncrementDifficulty();

        OnCitySpawned.Invoke();

        return spawnedCity;
    }

    private GameObject SetColour(GameObject city)
    {
        var color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        foreach (var child in city.transform.GetComponentsInChildren<Renderer>())
        {
            child.material.color = new Color(color.r, color.g, color.b, child.material.color.a);
        }

        return city;
    }

    private Vector3 GenerateSpawnPoint()
    {
        int angle = Random.Range(0, 360);

        var mineX = 2000 * Mathf.Cos(angle);
        var mineY = 2000 * Mathf.Sin(angle);

        return new Vector3(mineX, PlaneManager.Instance.GetCurrentPlaneHeight(), mineY);
    }

    private void IncrementDifficulty()
    {
        GameManager.Instance.DifficultyManager.IncrementSpawnedCities();

        MinSpawnTime = Mathf.Clamp(10 - GameManager.Instance.DifficultyManager.GetDifficultyMultiplier() * 2, 0.5f, 10);
        MaxSpawntime = Mathf.Clamp(10 - GameManager.Instance.DifficultyManager.GetDifficultyMultiplier(), 0.5f, 10);
    }

    public void DestroyCity(GameObject city)
    {
        SpawnedCities.Remove(city);

        Destroy(city);

        GameManager.Instance.ScoreKeeper.AddToScore(Enums.ScoreReward.DESTROYED_CITY);
    }
}