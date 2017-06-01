using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private bool IsHand(Collider other)
    {
        if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
            return true;
        else
            return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsHand(other))
        {
            Debug.Log("Yay! A hand collided!");
        }
    }
}
