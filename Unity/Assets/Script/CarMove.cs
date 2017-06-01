using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    [SerializeField]
    Transform[] _wheels;

    [SerializeField]
    GroundDetection[] _groundDetectors;

    // Update is called once per frame
    void Update ()
    {
        _wheels[0].Rotate(new Vector3(0, 1, 0), -180 * Time.deltaTime);
        for (int i  = 0; i < _wheels.Length; i++)
        {
            if (_groundDetectors[0].GetIsGrounded())
            {
                //gameObject.transform.
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-10, 0, 0));
                //_wheels[i].GetComponent<Rigidbody>().AddForce(new Vector3(-10, 0, 0));
            }
        }
	}
}