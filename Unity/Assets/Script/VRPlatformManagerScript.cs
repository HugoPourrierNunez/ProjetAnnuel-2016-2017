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

    //info pour le unclip
    private PlatformScript lastPlatformGrabAndUnClipped = null;
    private PlatformScript lastPlatformUnClipped=null;
    private bool connectedOnIn = true;
    private bool canReAttach = true;

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
        if(!canReAttach && lastPlatformGrabAndUnClipped!=null && lastPlatformUnClipped!=null)
        {
            if(connectedOnIn)
            {
                if(Vector3.Distance(lastPlatformGrabAndUnClipped.getGoInMarker().transform.position, lastPlatformUnClipped.getGoOutMarker().transform.position)> distanceClose*2)
                {
                    canReAttach = true;
                    grabScript.setUnclipped(false);
                }
            }
            else
            {
                if (Vector3.Distance(lastPlatformGrabAndUnClipped.getGoOutMarker().transform.position, lastPlatformUnClipped.getGoInMarker().transform.position) > distanceClose * 2)
                {
                    canReAttach = true;
                    grabScript.setUnclipped(false);
                }
            }
        }

        GameObject grabObject = grabScript.getPinchedPlatform();
        if (grabObject != null)
        {
            PlatformScript p = findPlatformScriptByGO(grabObject);
            if (p != null)
            {
                if (DetectNearestPieceAndAttach(p))
                {
                    grabScript.unGrab();
                    grabScript.setLastPlatformAttached(p);
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
        bool nearesrResult = true;

        for (int i = 0; i < allPlatform.Count; i++)
        {
            if (allPlatform[i].gameObject.activeInHierarchy && allPlatform[i]!= platform && !allPlatform[i].hasPlatforms())
            {
                
                float currentDist = RetrieveDistBetweenPiece(platform, allPlatform[i], out result);

                if (currentDist < nearest)
                {
                    nearest = currentDist;
                    nearestPlatform = allPlatform[i];
                    nearesrResult = result;
                }
            }
        }


        if(nearestPlatform != null)
        {
            if (nearest < distanceClose)
            {
                //a verifier
                if(nearesrResult)
                {
                    GameObject inGameObject = nearestPlatform.getGoInMarker();
                    GameObject outGameObject = platform.getGoOutMarker();

                    Vector3 vecPos = outGameObject.transform.position - inGameObject.transform.position;

                    platform.transform.position = platform.transform.position - vecPos;

                    nearestPlatform.setPlatformIn(platform);
                    platform.setPlatformOut(nearestPlatform);

                    PlatformScript nearestPlatform2 = null;
                    float nearest2 = float.MaxValue;

                    for (int i = 0; i < allPlatform.Count; i++)
                    {
                        if (allPlatform[i].gameObject.activeInHierarchy && allPlatform[i] != platform && allPlatform[i] != nearestPlatform)
                        {
                            float dist = Vector3.Distance(platform.getGoInMarker().transform.position, allPlatform[i].getGoOutMarker().transform.position);
                            if(dist<nearest2)
                            {
                                nearestPlatform2 = allPlatform[i];
                                nearest2 = dist;
                            }
                        }
                    }
                    if(nearest2<0.01)
                    {
                        nearestPlatform2.setPlatformOut(platform);
                        platform.setPlatformIn(nearestPlatform2);
                    }
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

                    PlatformScript nearestPlatform2 = null;
                    float nearest2 = float.MaxValue;

                    for (int i = 0; i < allPlatform.Count; i++)
                    {
                        if (allPlatform[i].gameObject.activeInHierarchy && allPlatform[i] != platform && allPlatform[i] != nearestPlatform)
                        {
                            float dist = Vector3.Distance(platform.getGoOutMarker().transform.position, allPlatform[i].getGoInMarker().transform.position);
                            if (dist < nearest2)
                            {
                                nearestPlatform2 = allPlatform[i];
                                nearest2 = dist;
                            }
                        }
                    }
                    if (nearest2 < 0.01)
                    {
                        nearestPlatform2.setPlatformIn(platform);
                        platform.setPlatformOut(nearestPlatform2);
                    }

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
                lastPlatformGrabAndUnClipped = p;
                if (p.getPlatformIn()!=null)
                {
                    lastPlatformUnClipped = p.getPlatformIn();
                    connectedOnIn = true;
                }
                else
                {
                    lastPlatformUnClipped = p.getPlatformOut();
                    connectedOnIn = false;
                }
                canReAttach = false;
                p.unclip();

                return true;
            }
        }
        return false;
    }

}
