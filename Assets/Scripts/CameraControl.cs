using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera MainCamera;

    public float CameraSpeedMultiplier;

    // Use this for initialization
    void Start ()
    {
        CameraSpeedMultiplier = 1.3F;
    }
	
	// Update is called once per frame
	void Update ()
    {
        MoveCamera();

        if (GetComponent<TurretController>().SinglePlayer)
        {
            swapToCamera();
        }
	}

    private void swapToCamera()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            MainCamera.enabled = false;

            GetComponent<TurretController>().CurrentTurret.GetComponentInChildren<Camera>().enabled = true;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            GetComponent<TurretController>().CurrentTurret.GetComponentInChildren<Camera>().enabled = false;

            MainCamera.enabled = true;
        }

        if (Input.GetKey(KeyCode.JoystickButton4))
        {
            MainCamera.enabled = false;
            GetComponent<TurretController>().CurrentTurret.GetComponentInChildren<Camera>().enabled = true;
        }
        else if(Input.GetKey(KeyCode.JoystickButton5))
        {
            GetComponent<TurretController>().CurrentTurret.GetComponentInChildren<Camera>().enabled = false;
            MainCamera.enabled = true;
        }
    }

    public void ChangeCamera(GameObject oldObject, GameObject newObject)
    {
        if(!MainCamera.isActiveAndEnabled)
        {
            oldObject.GetComponentInChildren<Camera>().enabled = false;
            newObject.GetComponentInChildren<Camera>().enabled = true;
        }
    }

    private void MoveCamera()
    {
        if(Input.GetAxis("HorizontalCamera") < 0)
        {
            MainCamera.transform.RotateAround(Vector3.zero, Vector3.up, System.Math.Abs(Input.GetAxis("HorizontalCamera") * CameraSpeedMultiplier));
        }
        else if(Input.GetAxis("HorizontalCamera") > 0)
        {
            MainCamera.transform.RotateAround(Vector3.zero, Vector3.up, (Input.GetAxis("HorizontalCamera") * CameraSpeedMultiplier) * -1);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            MainCamera.transform.RotateAround(Vector3.zero, Vector3.up, 1 * CameraSpeedMultiplier);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            MainCamera.transform.RotateAround(Vector3.zero, Vector3.up, -1 * CameraSpeedMultiplier);
        }
    }
}
