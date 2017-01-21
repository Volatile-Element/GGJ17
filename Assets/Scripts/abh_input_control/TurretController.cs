﻿using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject Turret1;
    public GameObject Turret2;
    public GameObject Turret3;
    public GameObject Turret4;
    public GameObject CurrentTurret;
    public GameObject CurrentTurretPlayerOne;
    public GameObject CurrentTurretPlayerTwo;
    public GameObject CurrentTurretPlayerThree;
    public GameObject CurrentTurretPlayerFour;

    public TurretIdentifierEnum.PlayerIdentifier TurretOneOwner;
    public TurretIdentifierEnum.PlayerIdentifier TurretTwoOwner;
    public TurretIdentifierEnum.PlayerIdentifier TurretThreeOwner;
    public TurretIdentifierEnum.PlayerIdentifier TurretFourOwner;

    public float SpeedMultiplier;
    public float WidthAdjustment;

    public int Direction;
    public bool Fire;

    public int DirectionPlayerOne;
    public bool FirePlayerOne;
    public int DirectionPlayerTwo;
    public bool FirePlayerTwo;
    public int DirectionPlayerThree;
    public bool FirePlayerThree;
    public int DirectionPlayerFour;
    public bool FirePlayerFour;

    public bool UseKeyboard;
    public bool UseXbox;
    public bool SinglePlayer;
    public bool MultiPlayer;

    // Use this for initialization
    void Start()
    {
        UseKeyboard = true;
        CurrentTurret = Turret1;
        CurrentTurretPlayerOne = Turret1;
        CurrentTurretPlayerTwo = null;
        CurrentTurretPlayerThree = null;
        CurrentTurretPlayerFour = null;
        TurretOneOwner = TurretIdentifierEnum.PlayerIdentifier.PlayerOne;
    }

    void Update()
    {
        if(SinglePlayer)
        {
            GetControlSinglePlayer();
            ChangeTurretSinglePlayer();
            CurrentTurret.transform.RotateAround(Vector3.zero, Vector3.up, Direction);
        }
        else if (MultiPlayer)
        {
            GetControlMultiplayer();
            ChangeTurretMultiPlayer();
            CurrentTurretPlayerOne.transform.RotateAround(Vector3.zero, Vector3.up, DirectionPlayerOne);

            if(CurrentTurretPlayerTwo != null)
            {
                CurrentTurretPlayerTwo.transform.RotateAround(Vector3.zero, Vector3.up, DirectionPlayerTwo);
            }

            if(CurrentTurretPlayerThree != null)
            {
                CurrentTurretPlayerThree.transform.RotateAround(Vector3.zero, Vector3.up, DirectionPlayerThree);
            }

            if(CurrentTurretPlayerFour != null)
            {
                CurrentTurretPlayerFour.transform.RotateAround(Vector3.zero, Vector3.up, DirectionPlayerFour);
            }
        }
    }

    private void ChangeOwners(TurretIdentifierEnum.PlayerIdentifier owner, GameObject turret)
    {
        if(turret != null)
        {
            switch(owner)
            {
                case TurretIdentifierEnum.PlayerIdentifier.PlayerOne:
                    CurrentTurretPlayerOne = turret;
                    break;
                case TurretIdentifierEnum.PlayerIdentifier.PlayerTwo:
                    CurrentTurretPlayerTwo = turret;
                    break;
                case TurretIdentifierEnum.PlayerIdentifier.PlayerThree:
                    CurrentTurretPlayerThree = turret;
                    break;
                case TurretIdentifierEnum.PlayerIdentifier.PlayerFour:
                    CurrentTurretPlayerFour = turret;
                    break;
            }

            if(turret.name == Turret1.name)
            {
                TurretOneOwner = owner;
            }
            else if(turret.name == Turret2.name)
            {
                TurretTwoOwner = owner;
            }
            else if(turret.name == Turret3.name)
            {
                TurretThreeOwner = owner;
            }
            else if(turret.name == Turret4.name)
            {
                TurretFourOwner = owner;
            }
        }
    }

    private void ChangeTurretMultiPlayer()
    {
        if (UseXbox)
        {
            CurrentTurretPlayerOne = MultipleUsersCheck("1", CurrentTurretPlayerOne, TurretIdentifierEnum.PlayerIdentifier.PlayerOne);
            CurrentTurretPlayerTwo = MultipleUsersCheck("2", CurrentTurretPlayerTwo, TurretIdentifierEnum.PlayerIdentifier.PlayerTwo);
            CurrentTurretPlayerThree = MultipleUsersCheck("3", CurrentTurretPlayerThree, TurretIdentifierEnum.PlayerIdentifier.PlayerThree);
            CurrentTurretPlayerFour = MultipleUsersCheck("4", CurrentTurretPlayerFour, TurretIdentifierEnum.PlayerIdentifier.PlayerFour);
        }
    }

    private GameObject MultipleUsersCheck(string playerNum, GameObject currentTurret, TurretIdentifierEnum.PlayerIdentifier player)
    {
        if(Input.GetKeyDown("joystick " + playerNum + " button 0"))
        {
            ChangeOwners(TurretOneOwner, currentTurret);

            TurretOneOwner = player;
            return Turret1;
        }
        else if(Input.GetKeyDown("joystick " + playerNum + " button 1"))
        {
            ChangeOwners(TurretTwoOwner, currentTurret);

            TurretTwoOwner = player;
            return Turret2;
        }
        else if(Input.GetKeyDown("joystick " + playerNum + " button 2"))
        {
            ChangeOwners(TurretThreeOwner, currentTurret);

            TurretThreeOwner = player;
            return Turret3;
        }
        else if(Input.GetKeyDown("joystick " + playerNum + " button 3"))
        {
            ChangeOwners(TurretFourOwner, currentTurret);

            TurretFourOwner = player;
            return Turret4;
        }

        return currentTurret;
    }

    private void ChangeTurretSinglePlayer()
    {
        if(UseKeyboard)
        {
            if (Input.GetKeyDown("q"))
            {
                ChangeTurrent(1);
            }
            else if (Input.GetKeyDown("w"))
            {
                ChangeTurrent(2);
            }
            else if (Input.GetKeyDown("e"))
            {
                ChangeTurrent(3);
            }
            else if (Input.GetKeyDown("r"))
            {
                ChangeTurrent(4);
            }
        }

        if (UseXbox)
        {
            if(Input.GetKeyDown("joystick button 0"))
            {
                ChangeTurrent(1);
            }
            else if(Input.GetKeyDown("joystick button 1"))
            {
                ChangeTurrent(2);
            }
            else if(Input.GetKeyDown("joystick button 2"))
            {
                ChangeTurrent(3);
            }
            else if(Input.GetKeyDown("joystick button 3"))
            {
                ChangeTurrent(4);
            }
        }
    }

    private int GetControllerMovement(string controlName)
    {
        if(Input.GetAxis(controlName) < 0)
        {
            return -1;
        }
        else if(Input.GetAxis(controlName) > 0)
        {
            return 1;
        }

        return 0;
    }

    private void GetControlMultiplayer()
    {
        if(UseXbox)
        {
            DirectionPlayerOne = GetControllerMovement("HorizontalControllerOne");
            DirectionPlayerTwo = GetControllerMovement("HorizontalControllerTwo");
            DirectionPlayerThree = GetControllerMovement("HorizontalControllerThree");
            DirectionPlayerFour = GetControllerMovement("HorizontalControllerFour");

            if (Input.GetAxis("RightTriggerControllerOne"))
            {
                CurrentTurretPlayerOne.GetComponent<Weapon>().Fire();
            }

            if(Input.GetAxis("RightTriggerControllerTwo"))
            {
                CurrentTurretPlayerTwo.GetComponent<Weapon>().Fire();
            }

            if(Input.GetAxis("RightTriggerControllerThree"))
            {
                CurrentTurretPlayerThree.GetComponent<Weapon>().Fire();
            }

            if(Input.GetAxis("RightTriggerControllerFour"))
            {
                CurrentTurretPlayerFour.GetComponent<Weapon>().Fire();
            }
        }
    }

    private void GetControlSinglePlayer()
    {
        if(UseKeyboard)
        {
            if(Input.GetKeyDown("left"))
            {
                Direction = -1;
            }
            else if(Input.GetKeyDown("right"))
            {
                Direction = 1;
            }
            else if(Input.GetKeyUp("left"))
            {
                Direction = 0;
            }
            else if(Input.GetKeyUp("right"))
            {
                Direction = 0;
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                CurrentTurret.GetComponent<Weapon>().Fire();
            }
        }

        if(UseXbox)
        {
            if(Input.GetAxis("Horizontal") < 0)
            {
                Direction = -1;
            }
            else if(Input.GetAxis("Horizontal") > 0)
            {
                Direction = 1;
            }
            else if(Input.GetAxis("Horizontal") == 0)
            {
                Direction = 0;
            }

            Fire = Input.GetAxis("RightTrigger") == 1 ? true : false;

            if (Fire)
            {
                CurrentTurret.GetComponent<Weapon>().Fire();
            }
        }
    }

    public void ChangeTurrent(int turrent)
    {
        switch (turrent)
        {
            case 1:
                CurrentTurret = Turret1;
                break;
            case 2:
                CurrentTurret = Turret2;
                break;
            case 3:
                CurrentTurret = Turret3;
                break;
            case 4:
                CurrentTurret = Turret4;
                break;
            default:
                break;
        }
    }
}