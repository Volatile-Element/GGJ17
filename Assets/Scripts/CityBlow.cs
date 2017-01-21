using UnityEngine;

public class CityBlow : MonoBehaviour {

    int RateOfBlow;
    int MaxToBlow;

	// Use this for initialization
	void Start ()
    {
        MaxToBlow = 100;
        GameManager.Instance.ScoreKeeper.ScoreChanged.AddListener(UpdateBlowMeter);
    }

    void UpdateBlowMeter()
    {
        if (RateOfBlow < MaxToBlow)
        {
            if (RateOfBlow + GameManager.Instance.ScoreKeeper.LastScoreAdded > 100)
            {
                RateOfBlow = MaxToBlow;
            }
            else
            {
                RateOfBlow += GameManager.Instance.ScoreKeeper.LastScoreAdded;
            }
        }
    }

    public void BlowUp()
    {
        if (RateOfBlow >= MaxToBlow)
        {
            Debug.Log("BOOOM");
        }
        else
        {
            Debug.Log("Nop");
        }        
    }
	
	// Update is called once per frame
	void UpdateStep ()
    {
		
	}
}
