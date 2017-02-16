using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    [SerializeField]
    GameObject goInMarker;

    [SerializeField]
    GameObject goOutMarker;

    [SerializeField]
    Transform cube;

    PlatformManagerScript platformManager;

    PlatformScript platformIn=null;
    PlatformScript platformOut=null;

    public void setPlatformManager(PlatformManagerScript manager)
    {
        platformManager = manager;
    }

    public PlatformScript getPlatformIn()
    {
        return platformIn;
    }

    public PlatformScript getPlatformOut()
    {
        return platformOut;
    }

    public bool hasPlatformIn()
    {
        return (platformIn != null);
    }

    public bool hasPlatformOut()
    {
        return (platformOut!= null);
    }

    public bool hasPlatform()
    {
        return (hasPlatformIn() || hasPlatformOut());
    }

    public void setPlatformIn(PlatformScript platform)
    {
        platformIn=platform;
    }

    public void setPlatformOut(PlatformScript platform)
    {
        platformOut = platform;
    }

    public GameObject getGoInMarker()
    {
        return goInMarker;
    }

    public GameObject getCube()
    {
        return cube.gameObject;
    }

    public GameObject getGoOutMarker()
    {
        return goOutMarker;
    }

    public void positionInOut()
    {
        goInMarker.transform.localPosition = new Vector3(cube.localScale.x/2, goInMarker.transform.localPosition.y, goInMarker.transform.localPosition.z);
        goOutMarker.transform.localPosition = new Vector3(-cube.localScale.x / 2, goOutMarker.transform.localPosition.y, goOutMarker.transform.localPosition.z);
    }



}
