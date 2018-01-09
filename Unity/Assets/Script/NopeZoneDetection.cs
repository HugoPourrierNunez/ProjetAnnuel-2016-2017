using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NopeZoneDetection : MonoBehaviour
{
    private int _platforms = 0;

    private void OnEnable()
    {
        _platforms = 0;
    }

    void OnTriggerEnter(Collider col)
    {
        _platforms++;
        Debug.Log(_platforms);
    }

    void OnTriggerExit(Collider col)
    {
        _platforms--;
        Debug.Log(_platforms);
    }

    public bool IsCrossed()
    {
        return (_platforms != 0);
    }
}
