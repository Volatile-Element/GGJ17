using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Distance = 50;
    public float Damage = 25;

    void Update()
    {
    }

	public void Fire()
    {
        Vector3 forward = transform.forward;
        Debug.DrawRay(transform.position, forward * 50, Color.green);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, forward, out hit, 50))
        {
            hit.transform.SendMessage("DealDamage", Damage);
        }
    }
}