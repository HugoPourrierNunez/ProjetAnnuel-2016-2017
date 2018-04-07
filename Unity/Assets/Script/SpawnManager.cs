using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    GameObject rightPinch;

    [SerializeField]
    GameObject leftPinch;

    [SerializeField]
    PinchDetector rightPinchDetector;

    [SerializeField]
    PinchDetector leftPinchDetector;

    [SerializeField]
    PlatformScript prefab;

    [SerializeField]
    GrabScript grabScript;

    [SerializeField]
    VRPlatformManagerScript platformManagerScript;

    [SerializeField]
    GameManager gameManager;

    private PlatformScript platform = null;
    private Vector3 position;

    bool isPinchLeft = false;
    bool isPinchRight = false;

    bool spawning = false;

    float lastSendTime = 0;
    float elapse = 1.0f;

    public bool isSpawning()
    {
        return spawning;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isPinchRight && isPinchLeft)
        {
            //Debug.Log(Vector3.Distance(rightPinch.transform.position, leftPinch.transform.position));
            if ( spawning == false && Vector3.Distance(rightPinch.transform.position, leftPinch.transform.position) < .04f && gameManager.getGamestate()==2)
            {
                //Debug.Log("Double Pinch");
                spawning = true;
                platform = platformManagerScript.getPlatform();

                /*platform.transform.localPosition = new Vector3(platform.transform.localPosition.x, 
                    platform.transform.localPosition.y, 
                    platform.transform.localPosition.z-(platform.getPlatformPrefabScript().getMeshFilter().mesh.bounds.size.z*platform.transform.localScale.z));*/

                Vector3 spawnPosition = Vector3.Lerp(rightPinch.transform.position, leftPinch.transform.position, .5f);
                platform.transform.position = spawnPosition;
                position = platform.transform.position;

            }
            if (spawning && platform!=null)
            {
                float distance = Mathf.Abs(rightPinch.transform.position.x - leftPinch.transform.position.x);
                //Debug.Log("distance x" + distance);
                Vector3 scale = new Vector3(distance*10, platform.getCube().transform.localScale.y, platform.getCube().transform.localScale.z);
                if (scale.x > 8)
                    scale.x = 8;

                platform.getCube().transform.localScale = scale;

                float timeStock = Time.timeSinceLevelLoad;
                //Debug.Log("timeStock=" + timeStock);
                //Debug.Log("lastSendTime=" + lastSendTime);
                if (timeStock-lastSendTime>elapse)
                {
                    //Debug.Log("Vibration spawn");
                    lastSendTime = timeStock;

                    double vibration = 100.0f + (scale.x * 155.0f / 8.0f);
                    //Debug.Log("vibration=" + vibration);

                    VibrorRequestScript.getInstance().SetIP(2);
                    VibrorRequestScript.getInstance().ChangeIntensityForFinger(new int[] { 3,5,9 }, (int) vibration);


                    VibrorRequestScript.getInstance().SetIP(1);
                    VibrorRequestScript.getInstance().ChangeIntensityForFinger(new int[] { 3, 5, 9 }, (int)vibration);
                }



                float diffX = rightPinch.transform.position.x - platform.transform.position.x;
                float diffY = rightPinch.transform.position.y - platform.transform.position.y;

                float distance2 = Mathf.Sqrt(diffX * diffX + diffY * diffY);

                float angle = Mathf.Rad2Deg * Mathf.Asin(Mathf.Abs(diffY) / Mathf.Abs(distance2));

                if (diffX > 0)
                {
                    if (diffY < 0)
                    {
                        angle = -angle;
                    }
                }
                else
                {
                    if (diffY > 0)
                    {
                        angle = 180 - angle;
                    }
                    else
                    {
                        angle = -180 + angle;
                    }
                }
                if (!System.Single.IsNaN(angle))
                {
                    platform.transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle-180);
                    platform.positionInOut();
                }

                //platform.transform.localPosition = new Vector3(position.x - (platform.getMeshFilter().mesh.bounds.size.x * platform.transform.localScale.x), platform.transform.localPosition.y, platform.transform.localPosition.z);

            }
        }
        else
        {
            if(spawning==true && platform!=null)
            {
                if (platform.getCube().transform.localScale.x < 1)
                    platform.unactive();

                VibrorRequestScript.getInstance().SetIP(2);
                VibrorRequestScript.getInstance().ChangeIntensityForFinger(new int[] { 3, 5, 9 }, 0);


                VibrorRequestScript.getInstance().SetIP(1);
                VibrorRequestScript.getInstance().ChangeIntensityForFinger(new int[] { 3, 5, 9 }, 0);
            }

            spawning = false;
            platform = null;


        }
	}

    public void positionInOut()
    {
        GameObject goInMarker = platform.getGoInMarker();
        GameObject goOutMarker = platform.getGoOutMarker();
        GameObject cube = platform.getCube();
        goInMarker.transform.localPosition = new Vector3(cube.transform.localScale.x/2, goInMarker.transform.localPosition.y, goInMarker.transform.localPosition.z);
        goOutMarker.transform.localPosition = new Vector3(-cube.transform.localScale.x / 2, goOutMarker.transform.localPosition.y, goOutMarker.transform.localPosition.z);
    }

    public void setIsPinchLeft(bool b)
    {
        //Debug.Log("setIsPinchLeft");
        isPinchLeft = b;
        //grabScript.updatePinchCollision();
    }

    public GameObject getSpawningGo()
    {
        if (platform == null)
        {
            return null;
        }
        return platform.gameObject;
    }

    public void setIsPinchRight(bool b)
    {
        //Debug.Log("setIsPinchRight");
        if (b == false)
            grabScript.setNeedUnpinch(false);

        isPinchRight = b;
        grabScript.updatePinchCollision();
    }

    public bool isRightPinchActive()
    {
        return isPinchRight;
    }

    public bool isLeftPinchActive()
    {
        return isPinchLeft;
    }
}
