using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AudioClip LazorCharge;
    public AudioClip LazorFire;

    public AudioClip PlasmaFire;

    public float Distance = 1000;
    public float Damage = 25;

    public float BallScale = 1;
    public GameObject Splosion;

    public Enums.FireType FireType;

    public GameObject LazorBall;
    public GameObject PlasmaBall;

    public AudioSource AudioSource;

    bool busy = false;

    bool firing = false;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
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
        else if(FireType == Enums.FireType.Plasma)
        {
            var pb = Instantiate(PlasmaBall, transform.position + transform.forward * 0.5f, transform.rotation);
            var cr = StartCoroutine("firePlasma", pb);
            StartCoroutine("wait");
        }
    }

    IEnumerator firePlasma(GameObject plasma)
    {
        AudioSource.PlayOneShot(PlasmaFire);
            while(!plasma.GetComponent<PlasmaBehaviour>().hitship)
            {
                if (Vector3.Distance(plasma.GetComponent<PlasmaBehaviour>().startPoint, plasma.transform.position) < 1000)
                {
                    var eh = plasma.transform.forward;
                    plasma.transform.Translate(Vector3.forward * 2);
                }
                yield return null;
            }
        var splosion = Instantiate(Splosion, plasma.transform.position, plasma.transform.rotation);
        StartCoroutine("Explode", splosion);
        yield return new WaitForSeconds(0.4f);
        Destroy(plasma);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        firing = false;
    }

    IEnumerator fireLazor(GameObject lazor)
    {

        while(firing)
        {
            AudioSource.clip = LazorCharge;
            AudioSource.Play();
            var growtarget = 20f * BallScale - 1;
            while (lazor.transform.localScale.x < growtarget)
            {
                lazor.transform.localScale = Vector3.Lerp(lazor.transform.localScale, new Vector3(20f * BallScale, 20f * BallScale, 20f * BallScale), 5f * BallScale * Time.deltaTime);
                yield return null;
            }
            AudioSource.Stop();

            var LazorLine = lazor.GetComponent<LineRenderer>();
            LazorLine.startWidth = 1 * BallScale;
            LazorLine.endWidth = 1.5f * BallScale;
            AudioSource.clip = LazorFire;
            AudioSource.Play();
            var rayPos = new Vector3(transform.position.x, transform.parent.position.y, transform.position.z);
            RaycastHit hit;
            Vector3 forward = transform.forward;
            Debug.DrawRay(rayPos, forward * Distance, Color.green);
            if (Physics.Raycast(rayPos, transform.forward, out hit, Distance))
            {
                var distance = Vector3.Distance(hit.transform.position, transform.position);
                LazorLine.SetPosition(1, new Vector3(0f,0f,distance*2));
                LazorLine.enabled = true;
                hit.transform.SendMessage("DealDamage", Damage);
                var splosion = Instantiate(Splosion, hit.transform.position, hit.transform.rotation);
                StartCoroutine("Explode", splosion);
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
            AudioSource.Stop();
            firing = false;
        }
    }

    bool Exploding = true;
    IEnumerator Explode(GameObject Explosion)
    {
        var ParticleSystems = Explosion.GetComponentsInChildren<ParticleSystem>();
        var psm = Explosion.GetComponent<ParticleSystemMultiplier>();
        while(psm.sploding)
        {
            psm.sploding = false;
            foreach(var p in ParticleSystems)
            {
                if (p.isPlaying)
                {
                    psm.sploding = true;
                }
            }

            yield return null;
        }

        if(psm.sploding == false)
        {
            DestroyObject(Explosion);
        }
        
    }
}