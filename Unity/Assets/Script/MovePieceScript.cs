using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MovePieceScript : MonoBehaviour {

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
        GameObject nearestPiece = DetectNearestPiece();
        float diff = RetrieveDistBetweenPiece(this.gameObject, nearestPiece);
        //float diff = (float)Math.Sqrt(Math.Pow(this.gameObject.transform.position.x - nearestPiece.transform.position.x, 2) + Math.Pow(this.gameObject.transform.position.y - nearestPiece.transform.position.y, 2) + Math.Pow(this.gameObject.transform.position.z - nearestPiece.transform.position.z, 2));

        if (diff < distanceClose && attached == false)
        {
            Debug.Log("test");
            GameObject inGameObject = FindingHierarchy.FindComponentInChildWithTag(nearestPiece, "In");
            GameObject outGameObject = FindingHierarchy.FindComponentInChildWithTag(this.gameObject, "Out");

            //outGameObject.transform.parent.position = outGameObject.transform.position - outGameObject.transform.localPosition;
            Vector3 vecPos = outGameObject.transform.localPosition;// this.gameObject.transform.localPosition;
           // Vector3 offsetPosOut = outGameObject.transform.localPosition;
            
            outGameObject.transform.position = inGameObject.transform.position;
            this.gameObject.transform.localPosition = outGameObject.transform.position - vecPos;
            //outGameObject.transform.parent.position = offsetPosOut;
            attached = true;
        }
    }

    float RetrieveDistBetweenPiece(GameObject pieceOne, GameObject pieceTwo)
    {
        GameObject inPieceTwo = FindingHierarchy.FindComponentInChildWithTag(pieceTwo, "In");
        GameObject outPieceOne = FindingHierarchy.FindComponentInChildWithTag(pieceOne, "Out");
        float dist = Math.Abs((float)Math.Sqrt(Math.Pow(outPieceOne.transform.position.x - inPieceTwo.transform.position.x, 2) + Math.Pow(outPieceOne.transform.position.y - inPieceTwo.transform.position.y, 2) + Math.Pow(outPieceOne.transform.position.z - inPieceTwo.transform.position.z, 2)));

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
                    nearest = currentDist;
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
