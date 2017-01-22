using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AICity : MonoBehaviour
{
    public Vector3 target;
    public CitySpawner CitySpawner;
    public int Health = 150;
    public float Speed = 20;
    public bool RotateRight;
    public float distanceToStop;

    public Weapon[] Weapons;
    public float WeaponSpeed = 10;
    public System.DateTime LastFired = System.DateTime.Now;
    public float FireSpeed = 3;
    public float FireDistance = 500;

    public AudioClip Siren;
    private AudioSource AudioSource;
    // Use this for initialization
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        CitySpawner = FindObjectOfType<CitySpawner>();
        distanceToStop = Random.Range(400, 490);
        if(Random.Range(0, 10) > 5)
        {
            RotateRight = true;
        }

        Weapons = transform.GetComponentsInChildren<Weapon>();
    }

    private void Movement()
    {
        if(Vector3.Distance(transform.position, target) < distanceToStop)
        {
            if(AudioSource.isPlaying == false)
            {
                AudioSource.clip = Siren;
                AudioSource.loop = true;
                AudioSource.Play();
            }
            
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
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        foreach (var weapon in Weapons)
        {
            weapon.transform.parent.RotateAround(weapon.transform.parent.position, transform.up, WeaponSpeed * Time.deltaTime);

            RaycastHit hit;
            Physics.Raycast(weapon.transform.position, weapon.transform.forward * FireDistance, out hit);
            Debug.DrawRay(weapon.transform.position, weapon.transform.forward * FireDistance, Color.blue);

            if (hit.collider == null)
            {
                continue;
            }

            var hitCity = hit.collider.gameObject.GetComponent<City>();
            if (hitCity != null && LastFired.AddSeconds(FireSpeed) < System.DateTime.Now)
            {
                Debug.Log("Enemy City Fired");
                weapon.Fire();
                LastFired = System.DateTime.Now;
            }
        }

        Movement();
    }

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
        AudioSource.Stop();
        CitySpawner.DestroyCity(gameObject);
        GameManager.Instance.DifficultyManager.IncrementDestroyedCities();
        
    }

    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SendMessage("DealDamage", 25);
        DestroyCity();
    }
}