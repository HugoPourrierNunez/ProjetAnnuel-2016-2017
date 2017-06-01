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

    [SerializeField]
    Renderer cubeRenderer;


    [SerializeField]
    Material selectedCubeMaterial;

    PlatformManagerScript platformManager;
    Material cubeMaterial = null;

    PlatformScript platformIn=null;
    PlatformScript platformOut=null;

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
        if (getPlatformIn())
            getPlatformIn().setPlatformOut(null);
        if (getPlatformOut())
            getPlatformOut().setPlatformIn(null);

        setPlatformIn(null);
        setPlatformOut(null);
    }

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
