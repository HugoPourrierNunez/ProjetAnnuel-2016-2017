using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBackdropColliderScript : MonoBehaviour
{

    private GameObject go = null;

    void OnCollisionEnter(Collision collision)
    {
        go = collision.collider.gameObject;
    }

    void OnCollisionExit(Collision collision)
    {
        go = null;
    }

    public GameObject getCollidedGO()
    {
        return go;
    }
}
