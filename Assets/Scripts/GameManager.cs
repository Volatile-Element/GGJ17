using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public DifficultyManager DifficultyManager = new DifficultyManager();
    public ScoreKeeper ScoreKeeper = new ScoreKeeper();

    protected GameManager() { } // So only Unity can create an instance.

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}