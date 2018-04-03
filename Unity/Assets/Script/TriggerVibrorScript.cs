using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class TriggerVibrorScript : MonoBehaviour {

    [SerializeField]
    int vibrationFrequency;

    [SerializeField]
    int vibrationTime;

    [SerializeField]
    string ip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {

        int handIndex = 0;

        if (col.gameObject.transform.parent.parent.gameObject.name == "RigidRoundHand_R")
        {
            handIndex = 2;
        }
        else if (col.gameObject.transform.parent.parent.gameObject.name == "RigidRoundHand_L")
        {
            handIndex = 1;
        }

        if (col.gameObject.name == "bone3" && handIndex != 0)
        {
            Debug.Log("IN");
            WebRequest request = null;
           
            if (col.gameObject.transform.parent.name == "thumb")
            {
                request = WebRequest.Create("http://" + ip + "/" + handIndex + "?1?" + vibrationFrequency + "?" + vibrationTime);
            }
            else if (col.gameObject.transform.parent.name == "index")
            {
                Debug.Log("Index");
                request = WebRequest.Create("http://" + ip + "/" + handIndex + "?2?" + vibrationFrequency + "?" + vibrationTime);
            }
            else if (col.gameObject.transform.parent.name == "middle")
            {
                request = WebRequest.Create("http://" + ip + "/" + handIndex + "?3?" + vibrationFrequency + "?" + vibrationTime);
            }
            else if (col.gameObject.transform.parent.name == "ring")
            {
                request = WebRequest.Create("http://" + ip + "/" + handIndex + "?4?" + vibrationFrequency + "?" + vibrationTime);
            }
            else if (col.gameObject.transform.parent.name == "pinky")
            {
                request = WebRequest.Create("http://" + ip + "/" + handIndex + "?5?" + vibrationFrequency + "?" + vibrationTime);
            }
            request.Timeout = 100;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }
    }
}
