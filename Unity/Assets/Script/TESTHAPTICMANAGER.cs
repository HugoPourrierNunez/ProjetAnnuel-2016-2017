using System.Collections;
using System.Net;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class TESTHAPTICMANAGER : MonoBehaviour {

    [SerializeField]
    private string ipLeftGlove = "127.0.0.1";

    [SerializeField]
    private string ipRightGlove = "127.0.0.1";

    private string ip = "127.0.0.1";
    private int intensityVibrationThumb = 0;
    private int intensityVibrationIndex = 0;
    private int intensityVibrationMiddle = 0;
    private int intensityVibrationRing = 0;
    private int intensityVibrationPinky = 0;
    int delay = 200;
    int timeStock = 0;
    private bool valueChanged = false;

    static private TESTHAPTICMANAGER instance;

    public static TESTHAPTICMANAGER getInstance()
    {
        return instance;
    }

    public void SetIP(int ipParam)
    {
        Debug.Log("IP changed to : " + ipParam);
        if (ipParam == 1)
            ip = ipLeftGlove;
        else if (ipParam == 2)
            ip = ipRightGlove;
    }

    public void ChangeIntensityForFinger(int[] pins, int intensity)
    {
        for (int i = 0; i < pins.Length; ++i)
        {
            Debug.Log("Changed pin intensity for pin : " + pins[i] + " with intensity " + intensity);
            switch (pins[i])
            {
                case 3:
                    intensityVibrationThumb = intensity;
                    break;
                case 5:
                    intensityVibrationIndex = intensity;
                    break;
                case 9:
                    intensityVibrationMiddle = intensity;
                    break;
                case 11:
                    intensityVibrationRing = intensity;
                    break;
                case 6:
                    intensityVibrationPinky = intensity;
                    break;
            }
        }
       
        valueChanged = true;
    }

    // Use this for initialization
    void Start ()
    {
        instance = this;
        this.timeStock = (int)(Time.timeSinceLevelLoad * 1000f) % 1000;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (valueChanged/* && ((int)(Time.timeSinceLevelLoad * 1000f) % 1000) - this.timeStock > 200*/)
        {
            Debug.Log("Send request");
            valueChanged = false;
            this.timeStock = (int)(Time.timeSinceLevelLoad * 1000f) % 1000;
            // format request : http://ip/intensityFinger1?intensityFinger2...

            // TCP
            Debug.Log("http://" + ip + "/" + intensityVibrationThumb + "?" + intensityVibrationIndex + "?" + intensityVibrationMiddle + "?" + intensityVibrationRing + "?" + intensityVibrationPinky);
            WebRequest request = WebRequest.Create("http://" + ip + "/" + intensityVibrationThumb + "?" + intensityVibrationIndex + "?" + intensityVibrationMiddle + "?" + intensityVibrationRing + "?" + intensityVibrationPinky);
            request.Proxy = null;
            request.Timeout = 50;
            WebResponse response = request.GetResponse();

            /* // UDP
            string strRequest = "http://" + ip + "/" + intensityVibrationThumb + "?" + intensityVibrationIndex + "?" + intensityVibrationMiddle + "?" + intensityVibrationRing + "?" + intensityVibrationPinky;
            UdpClient request = new UdpClient();
            byte[] data = Encoding.ASCII.GetBytes(strRequest);
            request.Send(data, data.Length, ip, 5001);*/
            Debug.Log("Finish to send");
        }
    }
}
