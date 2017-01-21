using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreController : MonoBehaviour
{
    Canvas Canvas;

	// Use this for initialization
	void Start ()
    {
        GameManager.Instance.ScoreKeeper.ScoreChanged.AddListener(UpdateScore);
        Canvas = GetComponent<Canvas>();
	}

    public void UpdateScore()
    {
        Canvas.transform.Find("Text - Score").GetComponent<Text>().text = "Score: " + GameManager.Instance.ScoreKeeper.CurrentScore;
    }
}