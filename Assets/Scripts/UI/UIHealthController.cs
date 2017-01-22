using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    Canvas Canvas;

    // Use this for initialization
    void Start()
    {
        FindObjectOfType<City>().OnHealthChanged.AddListener(UpdateHealth);
        Canvas = GetComponent<Canvas>();


        UpdateHealth();
    }

    public void UpdateHealth()
    {
        Canvas.transform.Find("Text - Health").GetComponent<Text>().text = "Health: " + FindObjectOfType<City>().Health;
    }
}