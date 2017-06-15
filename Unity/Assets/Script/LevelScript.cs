using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour {

    [SerializeField]
    PlatformScript start;

    public PlatformScript getPlatformStart()
    {
        return start;
    }

}
