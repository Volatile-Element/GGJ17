using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Vector3 target;
    public ShipSpawner ShipSpawner;
    public int Health = 50;
    public float Speed = 20;
    public bool LightsOn = false;
    SkyBoxController skyboxController;

    // Use this for initialization
    void Start ()
    {
        ShipSpawner = FindObjectOfType<ShipSpawner>();
        skyboxController = FindObjectOfType<SkyBoxController>();
    }
	
	void FixedUpdate ()
    {
        //I'm always moving at the minute, a state/curve may be needed further into the jam.
        transform.position += transform.forward * Speed * Time.deltaTime;
        LightChecker();
    }

    public void LightChecker()
    {
        if(skyboxController.DateSegment == "1" || skyboxController.DateSegment == "4" || skyboxController.DateSegment == "5")
        {
            GetComponentInChildren<Light>().enabled = true;
        }
        else
        {
            GetComponentInChildren<Light>().enabled = false;
        }
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
        ShipSpawner.DestoryShip(gameObject);
        GameManager.Instance.DifficultyManager.IncrementDestroyedShips();
    }

    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SendMessage("DealDamage", 25);
        DestroyShip();
    }
}