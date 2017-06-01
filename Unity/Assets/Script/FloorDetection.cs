using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetection : MonoBehaviour
{
    [SerializeField]
    GameManager gm;

	void OnTriggerEnter(Collider ball)
    {
        gm.Edit();
    }
}
