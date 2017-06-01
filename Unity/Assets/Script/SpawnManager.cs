using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    GameObject rightPinch;

    [SerializeField]
    GameObject leftPinch;

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
            if ( spawning == false && Vector3.Distance(rightPinch.transform.position, leftPinch.transform.position) < .02)
            {
                spawning = true;
                go = Instantiate(prefab.gameObject).GetComponent<PlatformPrefabScript>();
                go.transform.localPosition = new Vector3(go.transform.localPosition.x, 
                    go.transform.localPosition.y, 
                    go.transform.localPosition.z-(go.getMeshFilter().mesh.bounds.size.z*go.transform.localScale.z));
                go.transform.position = Vector3.Lerp(rightPinch.transform.position, leftPinch.transform.position, .5f);
                position = go.transform.position;
                Debug.Log("Double Pinch");
            }
            if (spawning && go!=null)
            {
                float distance = Mathf.Abs(rightPinch.transform.position.x - leftPinch.transform.position.x);
                Debug.Log("distance x" + distance);
                go.transform.localScale = new Vector3(distance*10, go.transform.localScale.y, go.transform.localScale.z);
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
        isPinchLeft = b;
    }

    public void setIsPinchRight(bool b)
    {
        isPinchRight = b;
    }
}
