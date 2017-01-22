using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveController : MonoBehaviour
{
    ShipSpawner ShipSpawner = new ShipSpawner();
    CitySpawner CitySpawner = new CitySpawner();

    public int CurrentWave = 0;

    public List<WaveItem> WaveItems = new List<WaveItem>();

    public WaveEvent OnWaveChange = new WaveEvent();

	// Use this for initialization
	void Start ()
    {
        ShipSpawner = FindObjectOfType<ShipSpawner>();

        StartCoroutine(SpawnWave());
    }

    private void GenerateWave()
    {
        var difficultyMultiplier = GameManager.Instance.DifficultyManager.GetDifficultyMultiplier();

        int miniWaves = 5 * difficultyMultiplier;
        
        for (int i = 0; i < miniWaves; i++)
        {
            for (int x = 0; x <= 5; x++)
            {
                WaveItems.Add(new WaveItem()
                {
                    SpawnType = Enums.EnemySpawnTypes.SHIP,
                    TimeTillNextSpawn = x == 5 ? 10 : Random.Range(0.5f, 2)
                });
            }
        }

        InsertCitiesIntoWave(difficultyMultiplier);
    }

    private void InsertCitiesIntoWave(int difficultyMutliplier)
    {
        int citiesToSpawn = (int)(difficultyMutliplier * 1.5f);

        int placementPoints = Mathf.Clamp((WaveItems.Count / citiesToSpawn) - 1, 0, int.MaxValue);

        int placedPoint = placementPoints;
        while (placedPoint < WaveItems.Count)
        {
            WaveItems.Add(new WaveItem()
            {
                SpawnType = Enums.EnemySpawnTypes.CITY,
                TimeTillNextSpawn = Random.Range(5f, 10f)
            });

            placedPoint += placementPoints;
        }
    }

    private void StartWave()
    {
        CurrentWave++;
        OnWaveChange.Invoke(CurrentWave);
        GenerateWave();
    }

    private void EndWave()
    {
        WaveItems.Clear();
    }

    public void ForceWaveEnd()
    {
        StopCoroutine(SpawnWave());
        WaveItems.Clear();
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        StartWave();

        foreach (var item in WaveItems)
        {
            switch (item.SpawnType)
            {
                case Enums.EnemySpawnTypes.SHIP:
                    ShipSpawner.SpawnShip();
                    break;
                case Enums.EnemySpawnTypes.CITY:
                    CitySpawner.SpawnCity();
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(item.TimeTillNextSpawn);
        }

        EndWave();

        StartCoroutine(SpawnWave());
    }
}

[System.Serializable]
public class WaveItem
{
    public Enums.EnemySpawnTypes SpawnType;
    public float TimeTillNextSpawn;
}