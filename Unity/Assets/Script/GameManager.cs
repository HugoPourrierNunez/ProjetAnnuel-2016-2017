using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TargetTrigger target;

    [SerializeField]
    Animator anim;

    [SerializeField]
    Rigidbody rigidBall;

    void Start()
    {
        rigidBall.isKinematic = false;
    }

    public void Win()
    {
        anim.SetBool("playMode", false);
        anim.SetBool("win", true);
    }

    public void Play()
    {
        rigidBall.isKinematic = false;
        anim.SetBool("editMode", false);
        anim.SetBool("playMode", true);
    }

    public void Edit()
    {
        if(anim.GetBool("playMode"))
        {
            anim.SetBool("playMode", false);
        }

        rigidBall.isKinematic = false;
        anim.SetBool("editMode", true);
    }
}
