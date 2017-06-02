using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    private bool isGrounded;

    void Start()
    {
        isGrounded = false;
    }

    void OnTriggerStay(Collider col)
    {
        isGrounded = true;
    }

    void OnTriggerExit(Collider col)
    {
        isGrounded = false;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }
}
