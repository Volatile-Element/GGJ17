using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class City : MonoBehaviour
{
    public int Health = 50;

    public Vector3 MoveTarget;
    public bool Moving;

    void Start()
    {
        PlaneManager.Instance.PlaneChanged.AddListener(ChangePlane);
    }

    void FixedUpdate()
    {
        if (Moving)
        {
            Move();
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
        SceneManager.LoadScene("Game Over");
    }

    private void ChangePlane()
    {
        MoveTarget = new Vector3(transform.position.x, PlaneManager.Instance.GetCurrentPlaneHeight(), transform.position.z);
        StartMove();
    }

    private void StartMove()
    {
        Moving = true;
    }
    private void Move()
    {
        float speed = 20;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, MoveTarget, step);

        if (transform.position == MoveTarget)
        {
            Moving = false;
        }
    }
}