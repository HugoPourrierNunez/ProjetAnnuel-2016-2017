using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class CollisionDetectorScript : MonoBehaviour {

    [SerializeField]
    private LeapRTSCustom leapRts;
   

    void OnCollisionEnter(Collision other)
    {
        if (IsHand(other))
        {
            Debug.Log("Yay! A hand collided!");
            leapRts.SetCollision(true);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (IsHand(other))
        {
            Debug.Log("Yay! A hand Exit Collider!");
            leapRts.SetCollision(false);
        }

        if (other.gameObject.tag == "Trash")
        {
            Debug.Log("Go to Trash !");
            leapRts.SetToTrash(true);
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
