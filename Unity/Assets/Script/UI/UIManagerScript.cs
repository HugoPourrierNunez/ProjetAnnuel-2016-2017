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

        VibrorRequestScript.getInstance().reset();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
