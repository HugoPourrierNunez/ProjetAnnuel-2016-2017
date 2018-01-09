using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCollision : MonoBehaviour {

    [SerializeField]
    GameManager gameManager;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Failed");
        gameManager.Edit();
    }
}
