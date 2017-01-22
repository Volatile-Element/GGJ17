using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DifficultyManager
{
    //Hello I manage how much of your life is hell!
    public float SpawnedShips;
    public float DestroyedShips;
    public System.DateTime GameStartTime;

    public DifficultyManager()
    {
        GameStartTime = GetCurrentDateTime();
    }

    public void IncrementSpawnedShips()
    {
        SpawnedShips++;
    }

    public void IncrementDestroyedShips()
    {
        DestroyedShips++;
    }

    public void IncrementSpawnedCities()
    {
        SpawnedShips++; //TODO: Add city logic.
    }

    public void IncrementDestroyedCities()
    {
        DestroyedShips++; //TODO: Add city logic.
    }

    public int GetDifficultyMultiplier()
    {
        var initial = (SpawnedShips + DestroyedShips) * (GetCurrentDateTime() - GameStartTime).TotalMinutes / SpawnedShips == 0 ? 1 : SpawnedShips;
        initial = Mathf.Clamp(initial, 1, float.MaxValue);

        return System.Convert.ToInt32(initial);
    }

    private System.DateTime GetCurrentDateTime()
    {
        return System.DateTime.Now;
    }
}