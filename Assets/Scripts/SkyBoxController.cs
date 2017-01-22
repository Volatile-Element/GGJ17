using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkyBoxController : MonoBehaviour
{
    Light[] lights;
    public Material Skybox1;
    public Material Skybox2;
    public Material Skybox3;
    public Material Skybox4;
    public Material Skybox5;
    public bool KeyOverride = false;

    public int ThreeAM = 3;
    public int SixAM = 6;
    public int NineAM = 9;
    public int SixPM = 18;
    public int NinePM = 21;

    public string DateSegment;

    // Use this for initialization
    void Start ()
    {
		lights = GetComponentsInChildren<Light>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        GetLightKeys();
        
        if(!KeyOverride)
        {
            GetLightsTime();
        }
    }

    private void GetLightsTime()
    {
        int hour = DateTime.Now.Hour;

        if (hour < ThreeAM)
        {
            UpdateSkybox("5", Skybox5);
        }
        else if (hour >= ThreeAM && hour < SixAM)
        {
            UpdateSkybox("1", Skybox1);
        }
        else if (hour >= SixAM && hour < NineAM)
        {
            UpdateSkybox("2", Skybox2);
        }
        else if (hour >= NineAM && hour < SixPM)
        {
            UpdateSkybox("3", Skybox3);
        }
        else if (hour >= SixPM && hour < NinePM)
        {
            UpdateSkybox("4", Skybox4);
        }
        else if (hour >= NinePM)
        {
            UpdateSkybox("5", Skybox5);
        }

    }

    private void UpdateSkybox(string endsNumber, Material skybox)
    {
        DateSegment = endsNumber;
        ResetLight();
        var light = lights.FirstOrDefault(x => x.name.EndsWith(endsNumber));

        light.enabled = true;
        RenderSettings.skybox = skybox;
        DynamicGI.UpdateEnvironment();
    }

    private void GetLightKeys()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            KeyOverride = true;
            UpdateSkybox("1", Skybox1);
        }
        else if(Input.GetKey(KeyCode.Alpha2))
        {
            KeyOverride = true;
            UpdateSkybox("2", Skybox2);
        }
        else if(Input.GetKey(KeyCode.Alpha3))
        {
            KeyOverride = true;
            UpdateSkybox("3", Skybox3);
        }
        else if(Input.GetKey(KeyCode.Alpha4))
        {
            KeyOverride = true;
            UpdateSkybox("4", Skybox4);
        }
        else if(Input.GetKey(KeyCode.Alpha5))
        {
            KeyOverride = true;
            UpdateSkybox("5", Skybox5);
        }
    }

    private void ResetLight()
    {
        foreach(var light in lights)
        {
            light.enabled = false;
        }
    }
}
