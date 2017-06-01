using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidBall;

    [SerializeField]
    GroundDetection ground;

    public float speed = 10.0f;

	void Update ()
    {
        if (ground.GetIsGrounded())
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 force = new Vector3(speed, 0.0f, 0.0f);
                rigidBall.AddForce(force, ForceMode.Force);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (rigidBall.velocity.x >= 0)
                {
                    Vector3 force = new Vector3(-speed, 0.0f, 0.0f);
                    rigidBall.AddForce(force, ForceMode.Force);
                }
            }
                }
    }
}
