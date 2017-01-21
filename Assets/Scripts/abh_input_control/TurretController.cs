using UnityEngine;

public class TurretController : MonoBehaviour {

    public GameObject Turret1;
    public GameObject Turret2;
    public GameObject Turret3;
    public GameObject Turret4;

    public float SpeedMultiplier;
    public float WidthAdjustment;

    // Use this for initialization
    void Start() {


    }

    // Update is called once per frame
    void FixedUpdate() {
            Turret1.transform.RotateAround(Vector3.zero, Vector3.up, CheckCollisions(TurretIdentifierEnum.TurretIdentifier.Turret1, 1));
        
    }

    public void RotateTurret(TurretIdentifierEnum.TurretIdentifier turret, float direction)
    {
        switch (turret)
        {
            case TurretIdentifierEnum.TurretIdentifier.Turret1:
                    Turret1.transform.RotateAround(Vector3.zero, Vector3.up, CheckCollisions(turret, direction));
                break;
            case TurretIdentifierEnum.TurretIdentifier.Turret2:
                    Turret2.transform.RotateAround(Vector3.zero, Vector3.up, CheckCollisions(turret, direction));
                break;
            case TurretIdentifierEnum.TurretIdentifier.Turret3:
                    Turret3.transform.RotateAround(Vector3.zero, Vector3.up, CheckCollisions(turret, direction));
                break;
            case TurretIdentifierEnum.TurretIdentifier.Turret4:
                    Turret4.transform.RotateAround(Vector3.zero, Vector3.up, CheckCollisions(turret, direction));
                break;
        }
    }

    private float CheckCollisions(TurretIdentifierEnum.TurretIdentifier movingObject, float direction)
    {
        var y1 = Turret1.transform.eulerAngles.y;
        var y2 = Turret2.transform.eulerAngles.y;
        var y3 = Turret3.transform.eulerAngles.y;
        var y4 = Turret4.transform.eulerAngles.y;

        var d = direction * Time.deltaTime * SpeedMultiplier;

        if(d > 0)
        {
            d = d + WidthAdjustment;
        }
        else
        {
            d = d - WidthAdjustment;
        }


        if (movingObject == TurretIdentifierEnum.TurretIdentifier.Turret1)
        {
            var endPos = y1 + d;
            if(endPos > 0)
            {
                if(endPos < y2)
                {
                    return endPos - y2;
                }
                if (endPos < y3)
                {
                    return endPos - y3;
                }
                if (endPos < y4)
                {
                    return endPos - y4;
                }
            }
            else
            {
                return 0;
            }
        }

        if (movingObject == TurretIdentifierEnum.TurretIdentifier.Turret2)
        {
            var endPos = y2 + d;
            return 0;
        }

        if (movingObject == TurretIdentifierEnum.TurretIdentifier.Turret3)
        {
            var endPos = y3 + d;
            return 0;
        }

        if(movingObject == TurretIdentifierEnum.TurretIdentifier.Turret4)
        {
            var endPos = y4 + d;
            return 0;
        }
        return 0;
    }
}
