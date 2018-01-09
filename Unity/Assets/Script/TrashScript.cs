using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour {

    [SerializeField]
    VRPlatformManagerScript vRPlatformManagerScript;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Piece")
        {
            vRPlatformManagerScript.unactivePlatform(other.gameObject.transform.parent.gameObject);
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.collider.gameObject.name);
    //    if (collision.collider.gameObject.tag == "Piece")
    //    {
    //        vRPlatformManagerScript.unactivePlatform(collision.collider.gameObject.transform.parent.gameObject);
    //    }
    //}
}
