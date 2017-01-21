using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Distance = 50;
    public float Damage = 25;

    public Enums.FireType FireType;

    public GameObject LazorBall;

    bool busy = false;

    bool firing = false;

    void Start()
    {
    }

    void Update()
    {

    }

	public void Fire()
    {
        if (firing)
            return;

        firing = true;


        if(FireType == Enums.FireType.Lazor)
        {
            var lb = Instantiate(LazorBall, transform.position + transform.forward * 0.5f , transform.rotation, transform);
            var cr = StartCoroutine("fireLazor", lb);
        }
    }

    IEnumerator fireLazor(GameObject lazor)
    {

        while(firing)
        {

            while (lazor.transform.localScale.x < 1.5f)
            {
                lazor.transform.localScale = Vector3.Lerp(lazor.transform.localScale, new Vector3(2.5f, 2.5f, 2.5f), 1f * Time.deltaTime);
                yield return null;
            }

            RaycastHit hit;
            Vector3 forward = transform.forward;
            Debug.DrawRay(transform.position, forward * Distance, Color.green);

            var LazorLine = lazor.GetComponent<LineRenderer>();
            if (Physics.Raycast(transform.position, transform.forward, out hit, Distance))
            {
                var distance = Vector3.Distance(hit.transform.position, transform.position);
                LazorLine.SetPosition(1, new Vector3(0f,0f,distance*2));
                LazorLine.enabled = true;
                hit.transform.SendMessage("DealDamage", Damage);
                yield return new WaitForSeconds(0.4f);
            }
            else
            {
                LazorLine.SetPosition(1, new Vector3(0f,0f,100f));
                LazorLine.enabled = true;
                yield return new WaitForSeconds(0.4f);
            }

            LazorLine.enabled = false;

            Destroy(lazor);
            firing = false;
        }
    }
}