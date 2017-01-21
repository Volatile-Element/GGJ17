using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class City : MonoBehaviour
{
    public int Health = 50;

    public void DealDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            DestroyCity();
        }
    }

    public void DestroyCity()
    {
        SceneManager.LoadScene("Game Over");
    }
}