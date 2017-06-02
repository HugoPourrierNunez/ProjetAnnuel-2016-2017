using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class PlatformPrefabScript : MonoBehaviour {

    [SerializeField]
    MeshFilter meshfilter;


    public MeshFilter getMeshFilter()
    {
        return meshfilter;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
