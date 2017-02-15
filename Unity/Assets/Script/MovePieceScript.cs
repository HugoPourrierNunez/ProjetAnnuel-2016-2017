using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MovePieceScript : MonoBehaviour {

    [SerializeField]
    public GameObject pieceToMove;

    [SerializeField]
    public GameObject otherPiece;

    [SerializeField]
    public float distanceClose;

    bool buttonDown;
    bool attached;

	// Use this for initialization
	void Start () {
        buttonDown = false;
        attached = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetMouseButtonDown(0))
        {
            buttonDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            buttonDown = false;
        }

        if (buttonDown == true && attached == false)
        {
            this.gameObject.transform.Translate(Input.GetAxis("Mouse X") / 3 , Input.GetAxis("Mouse Y") / 3, 0);
        }
        
        AttachToPiece();
	}

    void AttachToPiece()
    {
        float diff = (float)Math.Sqrt(Math.Pow(pieceToMove.transform.position.x - otherPiece.transform.position.x,2)+Math.Pow(pieceToMove.transform.position.y-otherPiece.transform.position.y,2)+Math.Pow(pieceToMove.transform.position.z-otherPiece.transform.position.z,2));

        if (diff < distanceClose && attached == false)
        {
            Debug.Log("test");
            GameObject inGameObject = FindingHierarchy.FindComponentInChildWithTag(otherPiece, "In");
            GameObject outGameObject = FindingHierarchy.FindComponentInChildWithTag(pieceToMove, "Out");

            //outGameObject.transform.parent.position = outGameObject.transform.position - outGameObject.transform.localPosition;
            Vector3 offsetPosOut = outGameObject.transform.localPosition;
            outGameObject.transform.position = inGameObject.transform.position;
            //outGameObject.transform.parent.position = offsetPosOut;
            attached = true;
        }
    }
}

public static class FindingHierarchy
{
    public static GameObject FindComponentInChildWithTag(this GameObject parent, string tag)
    {
        Transform t = parent.transform;

        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.gameObject;;
            }
        }
        return null;
    }
}
