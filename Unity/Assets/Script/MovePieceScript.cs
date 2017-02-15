using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MovePieceScript : MonoBehaviour {

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
        GameObject test = DetectNearestPiece();
        AttachToPiece();
	}

    void AttachToPiece()
    {
        float diff = (float)Math.Sqrt(Math.Pow(this.gameObject.transform.position.x - otherPiece.transform.position.x, 2) + Math.Pow(this.gameObject.transform.position.y - otherPiece.transform.position.y, 2) + Math.Pow(this.gameObject.transform.position.z - otherPiece.transform.position.z, 2));

        if (diff < distanceClose && attached == false)
        {
            Debug.Log("test");
            GameObject inGameObject = FindingHierarchy.FindComponentInChildWithTag(otherPiece, "In");
            GameObject outGameObject = FindingHierarchy.FindComponentInChildWithTag(this.gameObject, "Out");

            //outGameObject.transform.parent.position = outGameObject.transform.position - outGameObject.transform.localPosition;
            Vector3 offsetPosOut = outGameObject.transform.localPosition;
            outGameObject.transform.position = inGameObject.transform.position;
            //outGameObject.transform.parent.position = offsetPosOut;
            attached = true;
        }
    }

    float RetrieveDistBetweenPiece(GameObject pieceOne, GameObject pieceTwo)
    {
        float dist = Math.Abs((float)Math.Sqrt(Math.Pow(pieceOne.transform.position.x - pieceTwo.transform.position.x, 2) + Math.Pow(pieceOne.transform.position.y - pieceTwo.transform.position.y, 2) + Math.Pow(pieceOne.transform.position.z - pieceTwo.transform.position.z, 2)));

        return dist;
    }

    GameObject DetectNearestPiece()
    {
        GameObject result = null;
        GameObject[] listPieceInWorld = GameObject.FindGameObjectsWithTag("Piece");
        float nearest = 100000000000.0f;

        for (int i = 0; i < listPieceInWorld.Length; i++)
        {
            if(listPieceInWorld[i].transform.parent.gameObject.name != this.gameObject.transform.parent.gameObject.name)
            {
                float currentDist = this.RetrieveDistBetweenPiece(this.gameObject, listPieceInWorld[i]);

                if (currentDist < nearest)
                {
                    result = listPieceInWorld[i];
                }
            }
        }

        return result;
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
