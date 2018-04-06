using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVibrationScript : MonoBehaviour {

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
            //Debug.Log("Collision Enter pin : " + pin);
            //Debug.Log("Collision Enter IP : " + adresseGant);
            VibrorRequestScript.getInstance().SetIP(indexGlove);
            int[] pins = new int[1];
            pins[0] = pin;
            VibrorRequestScript.getInstance().ChangeIntensityForFinger(pins, intensity);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer == 5) // UI Layer
        {
            //Debug.Log("Collision Exit pin : " + pin);
            VibrorRequestScript.getInstance().SetIP(indexGlove);
            int[] pins = new int[1];
            pins[0] = pin;
            VibrorRequestScript.getInstance().ChangeIntensityForFinger(pins, 0);
        }
    }
}
