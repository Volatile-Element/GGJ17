﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Vector3 target;
    public ShipSpawner ShipSpawner;
    public int Health = 50;

    // Use this for initialization
    void Start ()
    {
        ShipSpawner = FindObjectOfType<ShipSpawner>();
    }
	
	void FixedUpdate ()
    {
        //I'm always moving at the minute, a state/curve may be needed further into the jam.
        transform.position += transform.forward * 10 * Time.deltaTime;
	}

    public void DealDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            DestroyShip();
        }
    }

    public void DestroyShip()
    {
        ShipSpawner.DestoryShip(this.gameObject);
        ShipSpawner.GameManager.DifficultyManager.IncrementDestroyedShips();
    }
}