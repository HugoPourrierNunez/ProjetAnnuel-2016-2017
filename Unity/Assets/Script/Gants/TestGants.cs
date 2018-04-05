using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class TestGants : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void click()
    {
        WebRequest request = null;
        request = WebRequest.Create("http://192.168.0.101/3?150?500");
        request.Timeout = 100;
        WebResponse response = request.GetResponse();

        //request = WebRequest.Create("http://192.168.0.101/5?150?500");
        //request.Timeout = 100;
        //response = request.GetResponse();

        //request = WebRequest.Create("http://192.168.0.101/9?150?500");
        //request.Timeout = 100;
        //response = request.GetResponse();

        //request = WebRequest.Create("http://192.168.0.101/6?150?500");
        //request.Timeout = 100;
        //response = request.GetResponse();

        //request = WebRequest.Create("http://192.168.0.101/11?150?500");
        //request.Timeout = 100;
        //response = request.GetResponse();
    }
}
