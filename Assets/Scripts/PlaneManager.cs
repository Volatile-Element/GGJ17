using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlaneManager : Singleton<PlaneManager>
{
    public Enums.GamePlane CurrentPlane;

    public UnityEvent PlaneChanged = new UnityEvent();

    public List<PlaneObject> PlaneObjects = new List<PlaneObject>();

    protected PlaneManager() { } // So only Unity can create an instance.

    void Start()
    {
        var planes = FindObjectsOfType<PlaneObject>();

        PlaneObjects.Add(planes.FirstOrDefault(x => x.GamePlane == Enums.GamePlane.SEA));
        PlaneObjects.Add(planes.FirstOrDefault(x => x.GamePlane == Enums.GamePlane.SKY));

        var waveManager = FindObjectOfType<WaveController>();
        if (waveManager != null)
        {
            waveManager.OnWaveChange.AddListener(TogglePlane);
        }
        else
        {
            Debug.LogWarning("A Wave Manager could not be found. Please add one to the scene if you want waves.");
        }
    }

    public void TogglePlane(int wave)
    {
        if (wave == 1)
        {
            return;
        }

        switch (CurrentPlane)
        {
            case Enums.GamePlane.SEA:
                CurrentPlane = Enums.GamePlane.SKY;
                break;
            case Enums.GamePlane.SKY:
                CurrentPlane = Enums.GamePlane.SEA;
                break;
            default:
                break;
        }

        PlaneChanged.Invoke();
    }

    public float GetCurrentPlaneHeight()
    {
        return PlaneObjects.FirstOrDefault(x => x.GamePlane == CurrentPlane).transform.position.y;
    }
}