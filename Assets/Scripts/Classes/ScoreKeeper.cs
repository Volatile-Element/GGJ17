using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreKeeper
{
    public int CurrentScore;
    public int ShipsDestroyed;
    public int LastScoreAdded;
    
    public UnityEvent ScoreChanged = new UnityEvent();

    public void AddToScore(int change)
    {
        LastScoreAdded = change;

        CurrentScore += change;

        ScoreChanged.Invoke();
    }

    public void AddToScore(Enums.ScoreReward scoreReward)
    {
        CurrentScore += (int)scoreReward;

        ScoreChanged.Invoke();
    }
}