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
    string ipDroite;

    [SerializeField]
    string ipGauche;

    string ip;

    [SerializeField]
    bool isLeft;

    [SerializeField]
    int fingerId;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {

        int handIndex = 0;

        if (!isLeft)
        {
            handIndex = 2;
        }
        else
        {
            handIndex = 1;
        }

        if (handIndex == 1)
        {
            ip = ipGauche;
        }
        else
            ip = ipDroite;

        if ( handIndex != 0)
        {
            Debug.Log("IN");
            WebRequest request = null;
            request.Proxy = null;
            request = WebRequest.Create("http://" + ip + "/" + fingerId + "?" + vibrationFrequency + "?" + vibrationTime);
            request.Timeout = 1;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }
    }
}
