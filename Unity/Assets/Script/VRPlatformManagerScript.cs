using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRPlatformManagerScript : MonoBehaviour {

    [SerializeField]
    PlatformScript prefab;

    List<PlatformScript> allPlatform = new List<PlatformScript>();

    [SerializeField]
    PlatformScript start;

    [SerializeField]
    int nbMaxPlatform = 10;

    [SerializeField]
    public float distanceClose=0.4f;

    [SerializeField]
    Text budgetTxt;

    [SerializeField]
    float totalBudget = 100.0f;

    [SerializeField]
    GrabScript grabScript;

    float usedBudget = 0.0f;

    // Use this for initialization
    void Start () {
        Debug.Log("start");
        if(start!=null)
        {
            allPlatform.Add(start);
            start.setPlatformOut(start);
        }
        for (int i=0;i<this.nbMaxPlatform; i++)
        {
            var p = Instantiate(prefab);
            p.gameObject.SetActive(false);
            p.setPlatformManager(this);
            p.transform.parent = transform;
            allPlatform.Add(p);
        }
    }



    public bool IsBudgetOver()
    {
        return (usedBudget > totalBudget);
    }

    // Update is called once per frame
    void Update () {
        GameObject grabObject = grabScript.getPinchedPlatform();
        if (grabObject != null)
        {
            PlatformScript p = findPlatformScriptByGO(grabObject);
            if (p != null)
            {
                if (DetectNearestPieceAndAttach(p))
                {
                    grabScript.unGrab();
                }
            }
        }
    }

    public PlatformScript findPlatformScriptByGO(GameObject go)
    {
        for(int i=0;i< allPlatform.Count; i++)
        {
            if(allPlatform[i].gameObject.activeInHierarchy)
            {
                if (allPlatform[i].gameObject == go)
                    return allPlatform[i];
            }
        }
        return null;
    }

    public PlatformScript getPlatform()
    {
        for (int i = 0; i < allPlatform.Count; i++)
        {
            if (!allPlatform[i].gameObject.activeInHierarchy)
            {
                allPlatform[i].gameObject.SetActive(true);
                return allPlatform[i];
            }
        }
        return null;
    }


    float RetrieveDistBetweenPiece(PlatformScript pieceOne, PlatformScript pieceTwo, out bool result)
    {
        result = true;
        float d1 = Vector3.Distance(pieceTwo.getGoInMarker().transform.position, pieceOne.getGoOutMarker().transform.position);
        float d2 = Vector3.Distance(pieceOne.getGoInMarker().transform.position, pieceTwo.getGoOutMarker().transform.position);

        if (d1 < d2)
            return d1;    
        result = false;
        return d2;
    }

    public bool DetectNearestPieceAndAttach(PlatformScript platform)
    {
        PlatformScript nearestPlatform = null;
        float nearest = float.MaxValue;
        bool result = true;

        for (int i = 0; i < allPlatform.Count; i++)
        {
            if (allPlatform[i].gameObject.activeInHierarchy && allPlatform[i]!= platform && !allPlatform[i].hasPlatforms())
            {
                
                float currentDist = RetrieveDistBetweenPiece(platform, allPlatform[i], out result);

                if (currentDist < nearest)
                {
                    nearest = currentDist;
                    nearestPlatform = allPlatform[i];
                }
            }
        }


        if(nearestPlatform != null)
        {
            if (nearest < distanceClose)
            {
                //a verifier
                if(result)
                {
                    GameObject inGameObject = nearestPlatform.getGoInMarker();
                    GameObject outGameObject = platform.getGoOutMarker();

                    Vector3 vecPos = outGameObject.transform.position - inGameObject.transform.position;

                    platform.transform.position = platform.transform.position - vecPos;

                    nearestPlatform.setPlatformIn(platform);
                    platform.setPlatformOut(nearestPlatform);
                    return true;

                }
                else
                {
                    GameObject inGameObject = platform.getGoInMarker();
                    GameObject outGameObject = nearestPlatform.getGoOutMarker();

                    Vector3 vecPos = outGameObject.transform.position - inGameObject.transform.position;

                    platform.transform.position = platform.transform.position + vecPos;

                    nearestPlatform.setPlatformOut(platform);
                    platform.setPlatformIn(nearestPlatform);
                    return true;
                }
            }
        }
        return false;
    }

    public void unactiveAllPlatform()
    {
        for (int i = 0; i < allPlatform.Count; i++)
        {
            allPlatform[i].unactive();
        }
    }
    
    public bool unclip(GameObject grabObject)
    {
        PlatformScript p = findPlatformScriptByGO(grabObject);
        if (p != null)
        {
            if (p.hasAPlatform())
            {
                p.unclip();
                return true;
            }
        }
        return false;
    }

}
