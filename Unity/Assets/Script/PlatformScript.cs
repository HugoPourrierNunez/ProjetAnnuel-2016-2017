using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Leap.Unity;

public class PlatformScript : MonoBehaviour {

    [SerializeField]
    GameObject goInMarker;

    [SerializeField]
    GameObject goOutMarker;

    [SerializeField]
    Transform cube;

    [SerializeField]
    Renderer cubeRenderer;


    [SerializeField]
    Material selectedCubeMaterial;

    VRPlatformManagerScript platformManager;
    Material cubeMaterial = null;

    PlatformScript platformIn=null;
    PlatformScript platformOut=null;

    [SerializeField]
    LeapRTSCustom leapRTSCustom;

    [SerializeField]
    PlatformPrefabScript platformPrefabScript;

    public LeapRTSCustom getLeapRTSCustom()
    {
        return leapRTSCustom;
    }
    public PlatformPrefabScript getPlatformPrefabScript()
    {
        return platformPrefabScript;
    }

    void Start()
    {
        cubeMaterial = cubeRenderer.material;
    }

    public void select(bool isSelected)
    {
        if(isSelected)
        {
            cubeRenderer.material = selectedCubeMaterial;
        }
        else
        {
            cubeRenderer.material = cubeMaterial;
        }
    }

    public void unactive()
    {
        gameObject.SetActive(false);

        unclip();

        cube.transform.localScale = new Vector3(1, cube.transform.localScale.y, cube.transform.localScale.z);
        transform.localRotation = new Quaternion();

        positionInOut();
    }

    public void unclip()
    {
        if (getPlatformIn()!=null)
            getPlatformIn().setPlatformOut(null);
        if (getPlatformOut()!=null)
            getPlatformOut().setPlatformIn(null);

        setPlatformIn(null);
        setPlatformOut(null);
    }

    public void setPlatformManager(VRPlatformManagerScript manager)
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

    public bool hasAPlatform()
    {
        return (hasPlatformIn() || hasPlatformOut());
    }

    public bool hasPlatforms()
    {
        return (hasPlatformIn() && hasPlatformOut());
    }

    public void setPlatformIn(PlatformScript platform)
    {
        platformIn=platform;
        if (platformIn == null)
            showMarkerIn();
        else
            hideMarkerIn();

        WebRequest request = null;
        request = WebRequest.Create("http://192.168.1.14/2?1?150?500");
        request.Timeout = 100;
        WebResponse response = request.GetResponse();
    }

    public void setPlatformOut(PlatformScript platform)
    {
        platformOut = platform;
        if (platformOut == null)
            showMarkerOut();
        else
            hideMarkerOut();
    }

    public void showMarkerIn()
    {
        goInMarker.SetActive(true);
    }

    public void hideMarkerIn()
    {
        goInMarker.SetActive(false);
    }

    public void showMarkerOut()
    {
        goOutMarker.SetActive(true);
    }

    public void hideMarkerOut()
    {
        goOutMarker.SetActive(false);
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
