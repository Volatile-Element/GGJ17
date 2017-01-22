using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBehaviour : MonoBehaviour {

    public GameObject CurrentCollision;

    public bool hitship;

    public Vector3 startPoint;

    public int Damage;

	// Use this for initialization
	void Start () {
        startPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {		
	}



    void OnTriggerEnter(Collider c)
    {
        CurrentCollision = c.gameObject;

        if(CurrentCollision.GetComponent<Ship>() != null || CurrentCollision.GetComponent<AICity>() != null)
        {
            hitship = true;
            c.transform.SendMessage("DealDamage", Damage);
        }
    }
}
