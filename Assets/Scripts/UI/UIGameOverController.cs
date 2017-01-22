using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOverController : MonoBehaviour
{
    public int score;

    public GameObject scoreDisplay;

    void Start()
    {
        score = PlayerPrefs.GetInt("score");
        scoreDisplay.GetComponent<Text>().text = score.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game View");
    }
}