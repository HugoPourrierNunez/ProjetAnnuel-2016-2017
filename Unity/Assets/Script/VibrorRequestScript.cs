using System.Collections;
using System.Net;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.Networking;

public class VibrorRequestScript : MonoBehaviour
{
    [SerializeField]
    bool activateLeft;

    [SerializeField]
    private string ipLeftGlove = "127.0.0.1";

    [SerializeField]
    bool activateRight;

    [SerializeField]
    private string ipRightGlove = "127.0.0.1";

    [SerializeField]
    private int delayUpdate = 1000;

    [SerializeField]
    private int delayMin = 50;

    private int intensityVibrationThumbRight = 0;
    private int intensityVibrationIndexRight = 0;
    private int intensityVibrationMiddleRight = 0;
    private int intensityVibrationRingRight = 0;
    private int intensityVibrationPinkyRight = 0;

    private int intensityVibrationThumbLeft = 0;
    private int intensityVibrationIndexLeft = 0;
    private int intensityVibrationMiddleLeft = 0;
    private int intensityVibrationRingLeft = 0;
    private int intensityVibrationPinkyLeft = 0;


    private static bool valueChanged = false;
    static int timer = 0;

    private int port = 5030;

    public static Mutex mute = new Mutex();
    static bool bb = false;
    Thread thread = null;
    static private VibrorRequestScript instance;
    static bool eventLaunched = false;
    private UdpClient client;
    //Thread thread;

    public static void vibration(int delay)
    {
        Thread.Sleep(delay);
        instance.sendVibration();
    }

    public void sendVibration()
    {
        byte[] data = null;
        string message = "";

        // UDP
        if (activateRight)
        {
            client.Connect(ipRightGlove, port);
            message = intensityVibrationThumbRight + ":" + intensityVibrationIndexRight + ":" + intensityVibrationMiddleRight + ":" + intensityVibrationRingRight + ":" + intensityVibrationPinkyRight;
            data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length);
        }

        if (activateLeft)
        {
            client.Connect(ipLeftGlove, port);
            message = intensityVibrationThumbLeft + ":" + intensityVibrationIndexRight + ":" + intensityVibrationMiddleLeft + ":" + intensityVibrationRingLeft + ":" + intensityVibrationPinkyLeft;
            data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length);
        }

        //Debug.Log(message + " - " + activateLeft + "--" + activateRight);
    }


    public static VibrorRequestScript getInstance()
    {
        return instance;
    }

    public void setFingerVibrationRight(int thumb, int index, int middle, int ring, int pinky)
    {
        intensityVibrationThumbRight = thumb;
        intensityVibrationIndexRight = index;
        intensityVibrationMiddleRight = middle;
        intensityVibrationRingRight = ring;
        intensityVibrationPinkyRight = pinky;

        valueChanged = true;
    }

    public void setFingerVibrationLeft(int thumb, int index, int middle, int ring, int pinky)
    {
        intensityVibrationThumbLeft = thumb;
        intensityVibrationIndexLeft = index;
        intensityVibrationMiddleLeft = middle;
        intensityVibrationRingLeft = ring;
        intensityVibrationPinkyLeft = pinky;

        valueChanged = true;
    }

    public void setOneFingerVibration(int value, int index, bool isRight)
    {
        if (isRight)
        {
            if (index == 0)
            {
                intensityVibrationThumbRight = value;
                valueChanged = true;
            }
            else if (index == 1)
            {
                intensityVibrationIndexRight = value;
                valueChanged = true;
            }
            else if (index == 2)
            {
                intensityVibrationMiddleRight = value;
                valueChanged = true;
            }
            else if (index == 3)
            {
                intensityVibrationRingRight = value;
                valueChanged = true;
            }
            else if (index == 4)
            {
                intensityVibrationPinkyRight = value;
                valueChanged = true;
            }
        }
        else
        {
            if (index == 0)
            {
                intensityVibrationThumbLeft = value;
                valueChanged = true;
            }
            else if (index == 1)
            {
                intensityVibrationIndexLeft = value;
                valueChanged = true;
            }
            else if (index == 2)
            {
                intensityVibrationMiddleLeft = value;
                valueChanged = true;
            }
            else if (index == 3)
            {
                intensityVibrationRingLeft = value;
                valueChanged = true;
            }
            else if (index == 4)
            {
                intensityVibrationPinkyLeft = value;
                valueChanged = true;
            }
        }
    }


    public void setFingerVibrationBoth(int thumb, int index, int middle, int ring, int pinky)
    {
        intensityVibrationThumbRight = thumb;
        intensityVibrationIndexRight = index;
        intensityVibrationMiddleRight = middle;
        intensityVibrationRingRight = ring;
        intensityVibrationPinkyRight = pinky;

        intensityVibrationThumbRight = thumb;
        intensityVibrationIndexRight = index;
        intensityVibrationMiddleRight = middle;
        intensityVibrationRingRight = ring;
        intensityVibrationPinkyRight = pinky;

        valueChanged = true;
    }


    public void reset()
    {
        intensityVibrationThumbRight = 0;
        intensityVibrationIndexRight = 0;
        intensityVibrationMiddleRight = 0;
        intensityVibrationRingRight = 0;
        intensityVibrationPinkyRight = 0;


        intensityVibrationThumbLeft = 0;
        intensityVibrationIndexLeft = 0;
        intensityVibrationMiddleLeft = 0;
        intensityVibrationRingLeft = 0;
        intensityVibrationPinkyLeft = 0;

        valueChanged = true;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;
        client = new UdpClient();
    }

    void Destroy()
    {
        //thread.Abort();
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)(Time.timeSinceLevelLoad * 1000f) - timer > delayMin)
        {
            timer = (int)(Time.timeSinceLevelLoad * 1000f);
            sendVibration();
            valueChanged = false;
        }
    }
}