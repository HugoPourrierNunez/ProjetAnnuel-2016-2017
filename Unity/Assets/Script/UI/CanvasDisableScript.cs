using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDisableScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDisable()
    {
        UIManagerScript.instance.checkUI(); 
    }
}
