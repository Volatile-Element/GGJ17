using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager
{
    //Hello I manage how much of your life is hell!
    public float SpawnedShips;
    public float DestroyedShips;
    public System.TimeSpan GameStartTime;

    public DifficultyManager()
    {
        GameStartTime = GetCurrentTimeSpan();
    }

    public void IncrementSpawnedShips()
    {
        SpawnedShips++;
    }

    public void IncrementDestroyedShips()
    {
        DestroyedShips++;
    }
    
    public int GetDifficultyMultiplier()
    {
        return System.Convert.ToInt32((SpawnedShips + DestroyedShips) * (GetCurrentTimeSpan().TotalMinutes - GameStartTime.TotalMinutes) / SpawnedShips);
    }

    private System.TimeSpan GetCurrentTimeSpan()
    {
        return new System.TimeSpan(System.DateTime.Now.Ticks);
    }

    private double GetCurrentTimeSpanMintues()
    {
        return GetCurrentTimeSpan().TotalMinutes;
    }
}