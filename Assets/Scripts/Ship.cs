using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    WaveController WaveController = new WaveController();

    public Vector3 target;
    public ShipSpawner ShipSpawner;
    public int Health = 50;
    public float Speed = 20;
    public float distanceToStop;
    public bool RotateRight;
    public bool Kamikaze;
    public bool LightsOn = false;
    SkyBoxController skyboxController;

    // Use this for initialization
    void Start ()
    {
        ShipSpawner = FindObjectOfType<ShipSpawner>();
        distanceToStop = Random.Range(200, 500);
        if(Random.Range(0,10) > 5)
        {
            RotateRight = true;
        }
        if(Random.Range(0,10) >= 9)
        {
            Kamikaze = true;
        }
        skyboxController = FindObjectOfType<SkyBoxController>();
        WaveController = FindObjectOfType<WaveController>();

        WaveController.OnWaveChange.AddListener(SetTarget);
    }
	
	void FixedUpdate ()
    {
        Movement();
        LightChecker();
    }

    private void Movement()
    {
        if(Vector3.Distance(transform.position, target) < distanceToStop && !Kamikaze)
        {
            var lookDir = Vector3.Cross(transform.position - target, Vector3.down);

            if (RotateRight)
            {
                lookDir = lookDir * -1;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDir, Vector3.up), 0.01f);

            if(RotateRight)
            {
                transform.RotateAround(Vector3.zero, Vector3.up, -0.07f);
            }
            else
            {
                transform.RotateAround(Vector3.zero, Vector3.up, 0.07f);
            }
        }
        else
        {
            //I'm always moving at the minute, a state/curve may be needed further into the jam.
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
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

    public void SetTarget(int wave)
    {
        target = new Vector3(0, PlaneManager.Instance.GetCurrentPlaneHeight(), 0);
    }
}