using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    public enum ScoreReward
    {
        DESTROYED_SHIP = 25,
        DESTROYED_CITY = 50,
        WAVE_COMPLETE = 100
    }

    public enum EnemySpawnTypes
    {
        SHIP,
        CITY
    }

    public enum GamePlane
    {
        SEA,
        SKY
    }
  
    public enum FireType
    {
        Lazor,
        Wave,
        Sound
    }
}