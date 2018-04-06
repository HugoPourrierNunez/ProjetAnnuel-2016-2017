using System.Collections;
using System.Net;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class VibrorRequestScript : MonoBehaviour {

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
    private static bool valueChanged = false;


    Mutex mute = new Mutex();

    static private VibrorRequestScript instance;

    //Thread thread;

    public static void vibration()
    {
        instance.sendVibration();
    }

    public void sendVibration()
    {

        /* Debug.Log("Send request 1");
         mute.WaitOne();
         Debug.Log("Send request 2");
         if (VibrorRequestScript.valueChanged == false)
         {
             mute.ReleaseMutex();
             Debug.Log("Send request 3");
             return;
         }

         valueChanged = false;

         mute.ReleaseMutex();*/


        Debug.Log("Send request 4");
        //this.timeStock = (int)(Time.timeSinceLevelLoad * 1000f) % 1000;
        // format request : http://ip/intensityFinger1?intensityFinger2...

        // TCP
        Debug.Log("http://" + ip + "/" + intensityVibrationThumb + "?" + intensityVibrationIndex + "?" + intensityVibrationMiddle + "?" + intensityVibrationRing + "?" + intensityVibrationPinky);
        WebRequest request = WebRequest.Create("http://" + ip + "/" + intensityVibrationThumb + "?" + intensityVibrationIndex + "?" + intensityVibrationMiddle + "?" + intensityVibrationRing + "?" + intensityVibrationPinky);
        request.Proxy = null;
        request.Timeout = 80;
        WebResponse response = request.GetResponse();

        /* // UDP
        string strRequest = "http://" + ip + "/" + intensityVibrationThumb + "?" + intensityVibrationIndex + "?" + intensityVibrationMiddle + "?" + intensityVibrationRing + "?" + intensityVibrationPinky;
        UdpClient request = new UdpClient();
        byte[] data = Encoding.ASCII.GetBytes(strRequest);
        request.Send(data, data.Length, ip, 5001);*/
        Debug.Log("Finish to send");
    }


    public static VibrorRequestScript getInstance()
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
            Debug.Log("Changed pin intensity for pin "+ Time.timeSinceLevelLoad + " : " + pins[i] + " with intensity " + intensity);
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
        Thread thread = new Thread(() => VibrorRequestScript.vibration());
        thread.Start();

        /*mute.WaitOne();
        valueChanged = true;
        mute.ReleaseMutex();*/
    }

    // Use this for initialization
    void Start ()
    {
        instance = this;
        this.timeStock = (int)(Time.timeSinceLevelLoad * 1000f) % 1000;
    }

    void Destroy()
    {
        //thread.Abort();
    }

        // Update is called once per frame
        void Update ()
    {
        
    }
}
