using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Leap;
using Leap.Unity;

public class RayCastLeap : MonoBehaviour
{

    HandModel hand_model;

    Hand leap_hand;
    private LeapProvider provider = null;

    private bool isHit;
    private bool alreadyHit = false;

    GameObject selectedObject;
    private int row;
    private int column;

    private Color previousColour;
    private Vector3 previousScale;


    void Start()
    {
        hand_model = GetComponent<HandModel>();
        provider = GetComponent<LeapServiceProvider>();
        leap_hand = hand_model.GetLeapHand();
        if (leap_hand == null) Debug.LogError("No leap_hand founded");
    }


    void Update()
    {
        FingerModel finger = hand_model.fingers[1];
        //Debug.DrawRay (finger.GetTipPosition(), finger.GetRay().direction, Color.red);
        RaycastHit hit;

        isHit = Physics.Raycast(finger.GetTipPosition(), finger.GetRay().direction, out hit);

        if (isHit)
        {
            if (hit.transform.gameObject == null)
            {
                selectedObject = null;
            }

            if (selectedObject != null && hit.transform.gameObject == selectedObject)
            {
                alreadyHit = true;
            }

            if (selectedObject != null && hit.transform.gameObject != selectedObject)
            {
                alreadyHit = false;
                selectedObject.transform.GetComponent<MeshRenderer>().material.color = previousColour;
                selectedObject.transform.localScale = previousScale;
                selectedObject = hit.transform.gameObject;
            }
            selectedObject = hit.transform.gameObject;
            if (selectedObject != null && !alreadyHit)
            {
                Debug.Log(selectedObject.name);
                Transform tmp = selectedObject.transform;
                MeshRenderer temp = selectedObject.GetComponent<MeshRenderer>();
                previousScale = tmp.localScale;
                previousColour = temp.material.color;
                tmp.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                temp.material.color = Color.red;
            }

            else if (selectedObject != null)
            {
                alreadyHit = false;
                selectedObject.transform.GetComponent<MeshRenderer>().material.color = previousColour;
                selectedObject.transform.localScale = previousScale;
                selectedObject = null;
            }

        }
    }
}