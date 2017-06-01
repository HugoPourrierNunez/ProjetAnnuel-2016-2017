using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour {

    [SerializeField]
    GameObject obstacle;

    [SerializeField]
    int typeMove;

    float i;
	// Use this for initialization
	void Start () {
        i = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        i += 0.1f;

        if (typeMove == 1)
        {
            obstacle.transform.position = new Vector3(obstacle.transform.position.x, obstacle.transform.position.y, 1 + obstacle.transform.position.z * Mathf.Cos(i));
        }
        else if(typeMove == 2)
        {
            obstacle.transform.position = new Vector3(obstacle.transform.position.x, 1 + obstacle.transform.position.y * Mathf.Cos(i),obstacle.transform.position.z);
        }
	}
}
