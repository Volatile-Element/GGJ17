using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICity : MonoBehaviour
{
    public Vector3 target;
    public CitySpawner CitySpawner;
    public int Health = 150;
    public float Speed = 20;

    public Weapon[] Weapons;
    public float WeaponSpeed = 10;
    public System.DateTime LastFired = System.DateTime.Now;
    public float FireSpeed = 3;
    public float FireDistance = 500;

    // Use this for initialization
    void Start()
    {
        CitySpawner = FindObjectOfType<CitySpawner>();

        Weapons = transform.GetComponentsInChildren<Weapon>();
    }

    void FixedUpdate()
    {
        //I'm always moving at the minute, a state/curve may be needed further into the jam.
        transform.position += transform.forward * Speed * Time.deltaTime;

        foreach (var weapon in Weapons)
        {
            weapon.transform.RotateAround(weapon.transform.parent.position, transform.up, WeaponSpeed * Time.deltaTime);

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
        CitySpawner.DestroyCity(gameObject);
        GameManager.Instance.DifficultyManager.IncrementDestroyedCities();
    }

    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SendMessage("DealDamage", 25);
        DestroyCity();
    }
}