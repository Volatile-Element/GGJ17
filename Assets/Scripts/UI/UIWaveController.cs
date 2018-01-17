using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWaveController : MonoBehaviour
{
    public Text WaveText;
    public Animator WaveAnimation;

	// Use this for initialization
	void Start ()
    {
        var child = transform.Find("Text - Wave #");
        WaveText = child.GetComponent<Text>();
        WaveAnimation = child.GetComponent<Animator>();

        FindObjectOfType<WaveController>().OnWaveChange.AddListener(ChangeWave);

        WaveAnimation.Play("Wave Idle");
    }
	
	public void ChangeWave(int wave)
    {
        WaveText.text = "Wave " + wave;
        WaveAnimation.Play("New Wave");
    }
}