using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour {

    private GameObject pinchedPlatform = null;
    private GameObject collidedPlatform = null;
    private bool unclipped = false;

    [SerializeField]
    SpawnManager spawnManager;

    [SerializeField]
    bool isRight;

    [SerializeField]
    VRPlatformManagerScript vRPlatformManagerScript;

    private bool needUnpinch = true;
    private GameObject lastPlatformAttached;

    public void setUnclipped(bool b)
    {
        unclipped = b;
    }

    public void setLastPlatformAttached(PlatformScript platform)
    {
        needUnpinch = true;
        lastPlatformAttached = platform.gameObject;
    }

    public void setNeedUnpinch(bool b)
    {
        needUnpinch = b;
    }

    public GameObject getPinchedPlatform()
    {
        if (unclipped)
            return null;
        return pinchedPlatform;
    }

    public void unGrab()
    {
        collidedPlatform = null;
        pinchedPlatform = null;
    }

    public void updatePinchCollision()
    {
        if (needUnpinch && collidedPlatform == lastPlatformAttached)
            return;

        if ((isRight && spawnManager.isRightPinchActive()) || (!isRight && spawnManager.isLeftPinchActive()))
        {
            pinchedPlatform = collidedPlatform;
            if (vRPlatformManagerScript.unclip(pinchedPlatform))
                unclipped = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(!spawnManager.isSpawning() && other.gameObject.tag == "Piece" && collidedPlatform== null)
        {
            collidedPlatform = other.gameObject.transform.parent.gameObject;
            updatePinchCollision();
        }
    }

    void OnTriggerExit(Collider other)
    {
        collidedPlatform = null;
        pinchedPlatform = null;
        unclipped = false;
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
