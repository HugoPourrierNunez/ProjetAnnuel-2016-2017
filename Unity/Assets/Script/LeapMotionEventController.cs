using Leap;
using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapMotionEventController : MonoBehaviour {

    //LeapProvider provider;

    void Start()
    {
        //provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    void Update()
    {
        /*Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsLeft)
            {
                transform.position = hand.PalmPosition.ToVector3() +
                                     hand.PalmNormal.ToVector3() *
                                    (transform.localScale.y * .5f + .02f);
                transform.rotation = hand.Basis.rotation.ToQuaternion();
            }
        }*/
    }

    public void extendedHand(bool b)
    {
        if(b)
            Debug.Log("extendedHand start");
        else
            Debug.Log("extendedHand end");
    }
}
