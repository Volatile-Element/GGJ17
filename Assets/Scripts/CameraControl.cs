using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera MainCamera;

    public float CameraSpeedMultiplier;

    public bool MultiplayerSplitScreen;

    // Use this for initialization
    void Start ()
    {
        MainCamera = Camera.main;
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

        if (MultiplayerSplitScreen)
        {
            SwapToCamerasMultiplayer();
        }
	}

    private void SwapToCamerasMultiplayer()
    {
        MainCamera.enabled = false;
        GetComponent<TurretController>().Turret1.GetComponentInChildren<Camera>().enabled = false;
        GetComponent<TurretController>().Turret2.GetComponentInChildren<Camera>().enabled = false;
        GetComponent<TurretController>().Turret3.GetComponentInChildren<Camera>().enabled = false;
        GetComponent<TurretController>().Turret4.GetComponentInChildren<Camera>().enabled = false;

        if(GetComponent<TurretController>().CurrentTurretPlayerOne != null
                && GetComponent<TurretController>().CurrentTurretPlayerTwo != null
                && GetComponent<TurretController>().CurrentTurretPlayerThree != null
                && GetComponent<TurretController>().CurrentTurretPlayerFour != null)
        {
            MainCamera.enabled = false;
            Rect playerOne = new Rect(0, 0.5f, 0.5f, 0.5f);
            Rect playerTwo = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            Rect playerThree = new Rect(0, 0, 0.5f, 0.5f);
            Rect playerFour = new Rect(0.5f, 0, 0.5f, 0.5f);

            GetComponent<TurretController>().CurrentTurretPlayerOne.GetComponentInChildren<Camera>().rect = playerOne;
            GetComponent<TurretController>().CurrentTurretPlayerTwo.GetComponentInChildren<Camera>().rect = playerTwo;
            GetComponent<TurretController>().CurrentTurretPlayerThree.GetComponentInChildren<Camera>().rect = playerThree;
            GetComponent<TurretController>().CurrentTurretPlayerFour.GetComponentInChildren<Camera>().rect = playerFour;

            GetComponent<TurretController>().CurrentTurretPlayerOne.GetComponentInChildren<Camera>().enabled = true;
            GetComponent<TurretController>().CurrentTurretPlayerTwo.GetComponentInChildren<Camera>().enabled = true;
            GetComponent<TurretController>().CurrentTurretPlayerThree.GetComponentInChildren<Camera>().enabled = true;
            GetComponent<TurretController>().CurrentTurretPlayerFour.GetComponentInChildren<Camera>().enabled = true;
        }
        else if(GetComponent<TurretController>().CurrentTurretPlayerOne != null
                && GetComponent<TurretController>().CurrentTurretPlayerTwo != null
                && GetComponent<TurretController>().CurrentTurretPlayerThree != null)
        {
            Rect playerOne = new Rect(0, 0.5f, 0.5f, 0.5f);
            Rect playerTwo = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            Rect playerThree = new Rect(0, 0, 0.5f, 0.5f);
            Rect playerFour = new Rect(0.5f, 0, 0.5f, 0.5f);

            GetComponent<TurretController>().CurrentTurretPlayerOne.GetComponentInChildren<Camera>().rect = playerOne;
            GetComponent<TurretController>().CurrentTurretPlayerTwo.GetComponentInChildren<Camera>().rect = playerTwo;
            GetComponent<TurretController>().CurrentTurretPlayerThree.GetComponentInChildren<Camera>().rect = playerThree;
            MainCamera.rect = playerFour;

            GetComponent<TurretController>().CurrentTurretPlayerOne.GetComponentInChildren<Camera>().enabled = true;
            GetComponent<TurretController>().CurrentTurretPlayerTwo.GetComponentInChildren<Camera>().enabled = true;
            GetComponent<TurretController>().CurrentTurretPlayerThree.GetComponentInChildren<Camera>().enabled = true;
            MainCamera.enabled = true;
        }
        else if(GetComponent<TurretController>().CurrentTurretPlayerOne != null 
                && GetComponent<TurretController>().CurrentTurretPlayerTwo != null)
        {
            MainCamera.enabled = false;
            Rect playerOne = new Rect(0, 0.5f, 1, 0.5f);
            Rect playerTwo = new Rect(0, 0, 1, 0.5f);

            GetComponent<TurretController>().CurrentTurretPlayerOne.GetComponentInChildren<Camera>().rect = playerOne;
            GetComponent<TurretController>().CurrentTurretPlayerTwo.GetComponentInChildren<Camera>().rect = playerTwo;

            GetComponent<TurretController>().CurrentTurretPlayerOne.GetComponentInChildren<Camera>().enabled = true;
            GetComponent<TurretController>().CurrentTurretPlayerTwo.GetComponentInChildren<Camera>().enabled = true;
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
