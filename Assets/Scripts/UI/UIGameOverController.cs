using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOverController : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("Game View");
    }
}