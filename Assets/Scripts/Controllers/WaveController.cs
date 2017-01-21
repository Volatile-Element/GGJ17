using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    ShipSpawner ShipSpawner = new ShipSpawner();

    public int CurrentWave = 0;

    public List<WaveItem> WaveItems = new List<WaveItem>();

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
    }

    private void StartWave()
    {
        CurrentWave++;
        GenerateWave();
    }

    private void EndWave()
    {
        WaveItems.Clear();
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

public class WaveItem
{
    public Enums.EnemySpawnTypes SpawnType;
    public float TimeTillNextSpawn;
}