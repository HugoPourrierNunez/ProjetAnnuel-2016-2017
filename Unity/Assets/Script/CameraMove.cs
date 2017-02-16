using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    Transform ballTransform;

    [SerializeField]
    Transform cameraTransform;
	
	// Update is called once per frame
	void Update ()
    {
        cameraTransform.position = new Vector3(ballTransform.position.x, ballTransform.position.y, -27);
	}
}
