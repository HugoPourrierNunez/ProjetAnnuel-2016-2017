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
    PlatformPrefabScript prefab;

    private PlatformPrefabScript go = null;
    private Vector3 position;

    bool isPinchLeft = false;
    bool isPinchRight = false;

    bool spawning = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isPinchRight && isPinchLeft)
        {
            Debug.Log(Vector3.Distance(rightPinch.transform.position, leftPinch.transform.position));
            if ( spawning == false && Vector3.Distance(rightPinch.transform.position, leftPinch.transform.position) < .04f)
            {
                Debug.Log("Double Pinch");
                spawning = true;
                go = Instantiate(prefab.gameObject).GetComponent<PlatformPrefabScript>();
                LeapRTSCustom aLeapRTSCustom = go.gameObject.GetComponent<LeapRTSCustom>();
                aLeapRTSCustom.SetSpawnManager(this);
                aLeapRTSCustom.setPinchDetector(leftPinchDetector, rightPinchDetector);

                go.transform.localPosition = new Vector3(go.transform.localPosition.x, 
                    go.transform.localPosition.y, 
                    go.transform.localPosition.z-(go.getMeshFilter().mesh.bounds.size.z*go.transform.localScale.z));
                go.transform.position = Vector3.Lerp(rightPinch.transform.position, leftPinch.transform.position, .5f);
                position = go.transform.position;
               
            }
            if (spawning && go!=null)
            {
                float distance = Mathf.Abs(rightPinch.transform.position.x - leftPinch.transform.position.x);
                Debug.Log("distance x" + distance);
                go.transform.localScale = new Vector3(distance*2, go.transform.localScale.y, go.transform.localScale.z);
                //go.transform.localPosition = new Vector3(position.x - (go.getMeshFilter().mesh.bounds.size.x * go.transform.localScale.x), go.transform.localPosition.y, go.transform.localPosition.z);

            }
        }
        else
        {
            spawning = false;
            go = null;
        }
	}

    public void setIsPinchLeft(bool b)
    {
        Debug.Log("setIsPinchLeft");
        isPinchLeft = b;
    }

    public GameObject getSpawningGo()
    {
        if (go == null)
        {
            return null;
        }
        return go.gameObject;
    }

    public void setIsPinchRight(bool b)
    {
        Debug.Log("setIsPinchRight");
        isPinchRight = b;
    }
}
