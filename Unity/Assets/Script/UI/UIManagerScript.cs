using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScript : MonoBehaviour {

    [SerializeField]
    TriggerVibrationScript[] fingers;

    public static UIManagerScript instance;
	// Use this for initialization
	void Start () {
        instance = this;
    }

    public void checkUI()
    {
        foreach(TriggerVibrationScript f in fingers)
        {

            Debug.Log("checkUI");
            Debug.Log("active="+ f.colision);
            if (f.colision!=null && !f.colision.gameObject.activeInHierarchy)
            {
                f.colision = null;
                int[] pins = { f.pin };
                VibrorRequestScript.getInstance().ChangeIntensityForFinger(pins, 0);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
