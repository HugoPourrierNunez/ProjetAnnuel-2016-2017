using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVibrationScript : MonoBehaviour {

    [SerializeField]
    int indexGlove = 1;

    [SerializeField]
    public int pin;

    [SerializeField]
    int intensity;

    public Collision colision;

    // Use this for initialization
    void Start () {
		
	}
    
	
	// Update is called once per frame
	void Update () {
        /*if (!gameObject.activeInHierarchy )
        {
            Debug.Log("disable");
            endCollision();
        }*/
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.layer == 5) // UI Layer
        {
            colision = col;
            Debug.Log("Collision Enter pin : " + pin);
            VibrorRequestScript.getInstance().SetIP(indexGlove);
            int[] pins = new int[1];
            pins[0] = pin;
            VibrorRequestScript.getInstance().ChangeIntensityForFinger(pins, intensity);
        }
    }

    void OnCollisionExit(Collision col)
    {
        //Debug.Log("OnCollisionExit");
        if (col.gameObject.layer == 5) // UI Layer
        {
            //Debug.Log("OnCollisionExit2");
            endCollision();
        }
    }

    private void OnDisable()
    {
        //Debug.Log("OnDisable");
    }

    public void endCollision()
    {
        if(colision!=null)
        {
            colision = null;
            //Debug.Log("Collision Exit pin : " + pin);
            VibrorRequestScript.getInstance().SetIP(indexGlove);
            int[] pins = new int[1];
            pins[0] = pin;
            VibrorRequestScript.getInstance().ChangeIntensityForFinger(pins, 0);
        }
        
    }

}
