using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityAlerts : MonoBehaviour {

    public AudioClip SpawnAlarm;
    public AudioClip AirSiren;

    private AudioSource AudioSource;
    

	// Use this for initialization
	void Start () {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = SpawnAlarm;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SoundAlarm()
    {
        AudioSource.clip = SpawnAlarm;
        AudioSource.loop = false;
        AudioSource.Play();
    }

    public void SoundSiren()
    {
        AudioSource.clip = AirSiren;
        AudioSource.loop = true;
        AudioSource.Play();
    }
}
