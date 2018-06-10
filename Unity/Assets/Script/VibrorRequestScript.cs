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
    static int timer = 0;

    Mutex mute = new Mutex();
    static bool bb = false;
    Thread thread = null;
    static private VibrorRequestScript instance;

    //Thread thread;

    public static void vibration(int delay)
    {
        Thread.Sleep(delay);
        instance.sendVibration();
    }

    public void sendVibration()
    {
        // TCP
        //Debug.Log("http://" + ip + "/?=" + intensityVibrationThumb + "&=" + intensityVibrationIndex + "&=" + intensityVibrationMiddle + "&=" + intensityVibrationRing + "&=" + intensityVibrationPinky);

        
        WWWForm form = new WWWForm();
        form.AddField("", "" + intensityVibrationThumb);
        form.AddField("", "" + intensityVibrationIndex);
        form.AddField("", "" + intensityVibrationMiddle);
        form.AddField("", "" + intensityVibrationRing);
        form.AddField("", "" + intensityVibrationPinky);
        

        UnityWebRequest uwr = UnityWebRequest.Post("http://" + ipRightGlove, form);
        uwr.Send();

        Debug.Log("Finish to send");
        timer = (int)(Time.timeSinceLevelLoad * 1000f);
    }


    public static VibrorRequestScript getInstance()
    {
        return instance;
    }

    public void SetIP(int ipParam)
    {
        //Debug.Log("IP changed to : " + ipParam);
        if (ipParam == 1)
            ip = ipLeftGlove;
        else if (ipParam == 2)
            ip = ipRightGlove;
    }

    public void ChangeIntensityForFinger(int[] pins, int intensity)
    {
        intensity *= 4;
        for (int i = 0; i < pins.Length; ++i)
        {
            //Debug.Log("Changed pin intensity for pin "+ Time.timeSinceLevelLoad + " : " + pins[i] + " with intensity " + intensity);
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
        /*Thread thread = new Thread(() => VibrorRequestScript.vibration());
        thread.Start();*/
       /* int res = (int)(Time.timeSinceLevelLoad * 1000f) - timer;
        if (res < 100)
        {
            Debug.Log("Test");

            if (thread != null)
            {
                thread.Abort();
            }

            thread = new Thread(() => VibrorRequestScript.vibration(100 - res));
            thread.Start();
        }
        else
        {
            thread = new Thread(() => VibrorRequestScript.vibration(0));
            thread.Start();
        }*/

        /*mute.WaitOne();
        valueChanged = true;
        mute.ReleaseMutex();*/
    }

    // Use this for initialization
    void Start()
    {
        instance = this;
        this.timeStock = (int)(Time.timeSinceLevelLoad * 1000f);
    }

    void Destroy()
    {
        //thread.Abort();
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)(Time.timeSinceLevelLoad * 1000f) - timer > 100)
        {
            timer = (int)(Time.timeSinceLevelLoad * 1000f);
            sendVibration();
        }
    }
}