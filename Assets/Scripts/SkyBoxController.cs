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

    // Use this for initialization
    void Start ()
    {
		lights = GetComponentsInChildren<Light>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ResetLight();
            var light = lights.FirstOrDefault(x => x.name.EndsWith("1"));

            light.enabled = true;
            RenderSettings.skybox = Skybox1;
            DynamicGI.UpdateEnvironment();
        }
        else if(Input.GetKey(KeyCode.Alpha2))
        {
            ResetLight();
            var light = lights.FirstOrDefault(x => x.name.EndsWith("2"));

            light.enabled = true;
            RenderSettings.skybox = Skybox2;
            DynamicGI.UpdateEnvironment();
        }
        else if(Input.GetKey(KeyCode.Alpha3))
        {
            ResetLight();
            var light = lights.FirstOrDefault(x => x.name.EndsWith("3"));

            light.enabled = true;
            RenderSettings.skybox = Skybox3;
            DynamicGI.UpdateEnvironment();
        }
        else if(Input.GetKey(KeyCode.Alpha4))
        {
            ResetLight();
            var light = lights.FirstOrDefault(x => x.name.EndsWith("4"));

            light.enabled = true;
            RenderSettings.skybox = Skybox4;
            DynamicGI.UpdateEnvironment();
        }
        else if(Input.GetKey(KeyCode.Alpha5))
        {
            ResetLight();
            var light = lights.FirstOrDefault(x => x.name.EndsWith("5"));

            light.enabled = true;
            RenderSettings.skybox = Skybox5;
            DynamicGI.UpdateEnvironment();
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
