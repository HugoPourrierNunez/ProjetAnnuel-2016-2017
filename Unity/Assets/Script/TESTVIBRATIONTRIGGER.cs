using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTVIBRATIONTRIGGER : MonoBehaviour {

    [SerializeField]
    int indexGlove = 1;

    [SerializeField]
    int pin;

    [SerializeField]
    int intensity;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.layer == 5) // UI Layer
        {
            Debug.Log("Collision Enter pin : " + pin);
            TESTHAPTICMANAGER.getInstance().SetIP(indexGlove);
            int[] pins = new int[1];
            pins[0] = pin;

            if(col.gameObject.name == "VibrorCubeHigh")
                TESTHAPTICMANAGER.getInstance().ChangeIntensityForFinger(pins, 250);
            if (col.gameObject.name == "VibrorCubeMiddle")
                TESTHAPTICMANAGER.getInstance().ChangeIntensityForFinger(pins, 125);
            if (col.gameObject.name == "VibrorCubeLow")
                TESTHAPTICMANAGER.getInstance().ChangeIntensityForFinger(pins, 80);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer == 5) // UI Layer
        {
            //Debug.Log("Collision Exit pin : " + pin);
            TESTHAPTICMANAGER.getInstance().SetIP(indexGlove);
            int[] pins = new int[1];
            pins[0] = pin;
            TESTHAPTICMANAGER.getInstance().ChangeIntensityForFinger(pins, 0);
        }
    }
}
