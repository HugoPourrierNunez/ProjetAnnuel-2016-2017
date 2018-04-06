using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScript : MonoBehaviour {

    [SerializeField]
    TriggerVibrationScript[] fingersRight;


    [SerializeField]
    TriggerVibrationScript[] fingersLeft;

    public static UIManagerScript instance;
	// Use this for initialization
	void Start () {
        instance = this;
    }

    public void reinitHands()
    {

        Debug.Log("reinitHands");

        List<int> pins = new List<int>();
        foreach (TriggerVibrationScript f in fingersRight)
        {
            pins.Add(f.pin);
            
        }
        if(fingersRight.Length>0)
        {
            VibrorRequestScript.getInstance().SetIP(2);
            VibrorRequestScript.getInstance().ChangeIntensityForFinger(pins.ToArray(), 0);
        }


        pins.Clear();
        foreach (TriggerVibrationScript f in fingersLeft)
        {
            pins.Add(f.pin);

        }
        if (fingersLeft.Length > 0)
        {
            VibrorRequestScript.getInstance().SetIP(1);
            VibrorRequestScript.getInstance().ChangeIntensityForFinger(pins.ToArray(), 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
