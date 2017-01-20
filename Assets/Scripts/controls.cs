using UnityEngine;

public class controls : MonoBehaviour {

    public int Direction;
    public bool Fire;

    public bool UseKeyboard;
    public bool UseXbox;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
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

            if(Input.GetKeyDown("space"))
            {
                Fire = true;
            }
            else if(Input.GetKeyUp("space"))
            {
                Fire = false;
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
        }
    }
}
