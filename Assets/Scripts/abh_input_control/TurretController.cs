using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject Turret1;
    public GameObject Turret2;
    public GameObject Turret3;
    public GameObject Turret4;
    public GameObject CurrentTurret;

    public float SpeedMultiplier;
    public float WidthAdjustment;

    public int Direction;
    public bool Fire;

    public bool UseKeyboard;
    public bool UseXbox;

    // Use this for initialization
    void Start()
    {
        UseKeyboard = true;
        CurrentTurret = Turret1;
    }

    void Update()
    {
        GetControl();
        ChangeTurret();
        CurrentTurret.transform.RotateAround(Vector3.zero, Vector3.up, Direction);
    }

    private void ChangeTurret()
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

    private void GetControl()
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

    // Update is called once per frame
    //void FixedUpdate() {
    //        Turret1.transform.RotateAround(Vector3.zero, Vector3.up, CheckCollisions(TurretIdentifierEnum.TurretIdentifier.Turret1, 1));
        
    //}

    //public void RotateTurret(TurretIdentifierEnum.TurretIdentifier turret, float direction)
    //{
    //    switch (turret)
    //    {
    //        case TurretIdentifierEnum.TurretIdentifier.Turret1:
    //                Turret1.transform.RotateAround(Vector3.zero, Vector3.up, CheckCollisions(turret, direction));
    //            break;
    //        case TurretIdentifierEnum.TurretIdentifier.Turret2:
    //                Turret2.transform.RotateAround(Vector3.zero, Vector3.up, CheckCollisions(turret, direction));
    //            break;
    //        case TurretIdentifierEnum.TurretIdentifier.Turret3:
    //                Turret3.transform.RotateAround(Vector3.zero, Vector3.up, CheckCollisions(turret, direction));
    //            break;
    //        case TurretIdentifierEnum.TurretIdentifier.Turret4:
    //                Turret4.transform.RotateAround(Vector3.zero, Vector3.up, CheckCollisions(turret, direction));
    //            break;
    //    }
    //}

    //private float CheckCollisions(TurretIdentifierEnum.TurretIdentifier movingObject, float direction)
    //{
    //    var y1 = Turret1.transform.eulerAngles.y;
    //    var y2 = Turret2.transform.eulerAngles.y;
    //    var y3 = Turret3.transform.eulerAngles.y;
    //    var y4 = Turret4.transform.eulerAngles.y;

    //    var d = direction * Time.deltaTime * SpeedMultiplier;

    //    if(d > 0)
    //    {
    //        d = d + WidthAdjustment;
    //    }
    //    else
    //    {
    //        d = d - WidthAdjustment;
    //    }


    //    if (movingObject == TurretIdentifierEnum.TurretIdentifier.Turret1)
    //    {
    //        var endPos = y1 + d;
    //        if(endPos > 0)
    //        {
    //            if(endPos < y2)
    //            {
    //                return endPos - y2;
    //            }
    //            if (endPos < y3)
    //            {
    //                return endPos - y3;
    //            }
    //            if (endPos < y4)
    //            {
    //                return endPos - y4;
    //            }
    //        }
    //        else
    //        {
    //            return 0;
    //        }
    //    }

    //    if (movingObject == TurretIdentifierEnum.TurretIdentifier.Turret2)
    //    {
    //        var endPos = y2 + d;
    //        return 0;
    //    }

    //    if (movingObject == TurretIdentifierEnum.TurretIdentifier.Turret3)
    //    {
    //        var endPos = y3 + d;
    //        return 0;
    //    }

    //    if(movingObject == TurretIdentifierEnum.TurretIdentifier.Turret4)
    //    {
    //        var endPos = y4 + d;
    //        return 0;
    //    }
    //    return 0;
    //}
}
