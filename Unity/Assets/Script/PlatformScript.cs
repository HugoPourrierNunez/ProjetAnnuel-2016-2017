using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    [SerializeField]
    GameObject goIn;

    [SerializeField]
    GameObject goOut;

    [SerializeField]
    Transform cube;

    PlatformManagerScript platformManager;

    bool clipped = false;

    public void setPlatformManager(PlatformManagerScript manager)
    {
        platformManager = manager;
    }

    public GameObject getGoIn()
    {
        return goIn;
    }

    public GameObject getCube()
    {
        return cube.gameObject;
    }

    public GameObject getGoOut()
    {
        return goOut;
    }

    public bool isClipped()
    {
        return clipped;
    }

    public void setClipped(bool aClipped)
    {
        clipped = aClipped;
    }

    public void positionInOut()
    {
        goIn.transform.localPosition = new Vector3(cube.localScale.x/2, goIn.transform.localPosition.y, goIn.transform.localPosition.z);
        goOut.transform.localPosition = new Vector3(-cube.localScale.x / 2, goOut.transform.localPosition.y, goOut.transform.localPosition.z);
    }



}
