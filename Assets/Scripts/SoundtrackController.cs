using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackController : MonoBehaviour {

    public List<AudioClip> SoundTrack;

    private AudioSource Player;

	// Use this for initialization
	void Start () {
        Player = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(!Player.isPlaying)
        {
            var clip = Random.Range(0, SoundTrack.Count);
            Player.clip = SoundTrack[clip];
            Player.Play();
        }

	}
}
