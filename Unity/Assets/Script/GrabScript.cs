using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour {

    private GameObject pinchedPlatform = null;
    private GameObject collidedPlatform = null;

    [SerializeField]
    SpawnManager spawnManager;

    [SerializeField]
    bool isRight;

    public void updatePinchCollision()
    {
        if ((isRight && spawnManager.isRightPinchActive()) || (!isRight && spawnManager.isLeftPinchActive()))
        {
            pinchedPlatform = collidedPlatform;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(!spawnManager.isSpawning() && other.gameObject.tag == "Piece")
        {
            collidedPlatform = other.gameObject.transform.parent.gameObject;
            updatePinchCollision();
        }
    }

    void OnTriggerExit(Collider other)
    {
        collidedPlatform = null;
        pinchedPlatform = null;
    }


    // Update is called once per frame
    void Update () {
        if ((isRight && !spawnManager.isRightPinchActive()) || (!isRight && !spawnManager.isLeftPinchActive()))
        {
            pinchedPlatform = null;
        }
        if(pinchedPlatform!=null)
        {
            pinchedPlatform.transform.position = transform.position;
        }
	}
}
