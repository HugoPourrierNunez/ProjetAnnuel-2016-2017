using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class CollisionDetectorScript : MonoBehaviour {

    [SerializeField]
    private LeapRTSCustom leapRts;
   

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hello");
        if (IsHand(other))
        {
            Debug.Log("Yay! A hand collided!");
            leapRts.SetCollision(true);
        }
    }

    void OnCollisionExit(Collision other)
    {
        Debug.Log("Hello");
        if (IsHand(other))
        {
            Debug.Log("Yay! A hand Exit Collider!");
            leapRts.SetCollision(false);
        }
    }

    private bool IsHand(Collision other)
    {
        if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
            return true;
        else
            return false;
    }
}
