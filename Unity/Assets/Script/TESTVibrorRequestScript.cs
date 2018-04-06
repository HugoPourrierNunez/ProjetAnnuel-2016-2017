using System.Collections;
using System.Net;
using System.Collections.Generic;
using UnityEngine;

public class TESTVibrorRequestScript : MonoBehaviour {

    [SerializeField]
    private int intensityVibrationThumbPub;

    [SerializeField]
    private int intensityVibrationIndexPub;

    [SerializeField]
    private int intensityVibrationMiddlePub;

    [SerializeField]
    private int intensityVibrationRingPub;

    [SerializeField]
    private int intensityVibrationPinkyPub;

    [SerializeField]
    private string ip;


    private int intensityVibrationThumb;
    private int intensityVibrationIndex;
    private int intensityVibrationMiddle;
    private int intensityVibrationRing;
    private int intensityVibrationPinky;

    private bool valueChanged = false;

    //int delay = 200;
    //int delaySince = Time.time;
    // Use this for initialization
    void Start ()
    {
        intensityVibrationThumb = intensityVibrationThumbPub;
        intensityVibrationIndex = intensityVibrationIndexPub;
        intensityVibrationMiddle = intensityVibrationMiddlePub;
        intensityVibrationRing = intensityVibrationRingPub;
        intensityVibrationPinky = intensityVibrationPinkyPub;

        WebRequest request = null;
        request.Proxy = null;
        request = WebRequest.Create("http://" + ip + "/" + intensityVibrationThumb + "?" + intensityVibrationIndex + "?" + intensityVibrationMiddle + "?" + intensityVibrationRing + "?" + intensityVibrationPinky);
        request.Timeout = 10;
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(intensityVibrationThumb != intensityVibrationThumbPub ||
            intensityVibrationIndex != intensityVibrationIndexPub ||
            intensityVibrationMiddle != intensityVibrationMiddlePub ||
            intensityVibrationRing != intensityVibrationRingPub ||
            intensityVibrationPinky != intensityVibrationPinkyPub)
        {
            intensityVibrationThumb = intensityVibrationThumbPub;
            intensityVibrationIndex = intensityVibrationIndexPub;
            intensityVibrationMiddle = intensityVibrationMiddlePub;
            intensityVibrationRing = intensityVibrationRingPub;
            intensityVibrationPinky = intensityVibrationPinkyPub;

            valueChanged = true;
        }

        if (valueChanged)
        {
            // format request : http://ip/intensityFinger1?intensityFinger2...
            WebRequest request = null;
            request.Proxy = null;
            request = WebRequest.Create("http://" + ip + "/" + intensityVibrationThumb + "?" + intensityVibrationIndex + "?" + intensityVibrationMiddle + "?" + intensityVibrationRing + "?" + intensityVibrationPinky);
            request.Timeout = 10;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }
    }
}
