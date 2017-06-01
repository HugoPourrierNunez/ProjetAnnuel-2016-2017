using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManagerScript : MonoBehaviour {

    [SerializeField]
    PlatformScript prefab;

    List<PlatformScript> allPlatform = new List<PlatformScript>();

    [SerializeField]
    float timeDoubleClick = .2f;

    [SerializeField]
    PlatformScript start;

    [SerializeField]
    int nbMaxPlatform = 10;

    [SerializeField]
    Camera worlCamera;

    [SerializeField]
    MeshCollider spawnColliderQuad;

    [SerializeField]
    GameObject indexL;

    [SerializeField]
    GameObject indexR;

    [SerializeField]
    public float distanceClose=0.4f;

    float doubleLeftClickStart = 0;
    float doubleRightClickStart = 0;

    bool buttonDown=false;

    private bool activeLeftPinch = false;
    private bool activeRightPinch = false;
    private bool objectInCreation = false;

    PlatformScript platformSpawning = null;

    PlatformScript platformPointing = null;

    PlatformScript platformDragging = null;

    bool canClip = false;

    // Use this for initialization
    void Start () {
        Debug.Log("start");

        allPlatform.Add(start);
        start.setPlatformOut(start);
        for (int i=1;i<this.nbMaxPlatform; i++)
        {
            var p = Instantiate(prefab);
            p.gameObject.SetActive(false);
            p.setPlatformManager(this);
            p.transform.parent = transform;
            allPlatform.Add(p);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if(platformSpawning != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit info;

            if (spawnColliderQuad.Raycast(ray, out info, 1000))
            {
                float diffX = info.point.x - platformSpawning.transform.position.x;
                float diffY = info.point.y - platformSpawning.transform.position.y;

                float distance = Mathf.Sqrt(diffX* diffX + diffY* diffY);

                platformSpawning.getCube().transform.localScale = new Vector3(distance*2, platformSpawning.transform.localScale.y, platformSpawning.transform.localScale.z);

                float angle = Mathf.Rad2Deg * Mathf.Asin(Mathf.Abs(diffY) / Mathf.Abs(distance));

                if (diffX>0)
                {
                    if(diffY<0)
                    {
                        angle = -angle;
                    }
                }
                else
                {
                    if (diffY > 0)
                    {
                        angle = 180 - angle;
                    }
                    else
                    {
                        angle = -180+angle;
                    }
                }
                if(!System.Single.IsNaN(angle))
                {
                    platformSpawning.transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle);
                    platformSpawning.positionInOut();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if(platformSpawning!=null && platformSpawning.getCube().transform.localScale.x>1)
                {
                    platformSpawning.positionInOut();
                    platformSpawning = null;
                }
            }

            if (this.activeLeftPinch == true && this.activeRightPinch == true)
            {
                if (platformSpawning != null && platformSpawning.getCube().transform.localScale.x > 1)
                {
                    platformSpawning.positionInOut();
                    platformSpawning = null;
                }
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            spawnColliderQuad.gameObject.SetActive(false);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Piece")
                {
                    if (platformPointing != null)
                        platformPointing.select(false);
                    platformPointing = findPlatformScriptByCubeGO(hit.collider.gameObject);
                    platformPointing.select(true);
                }
            }
            else if (platformPointing != null)
            {
                platformPointing.select(false);
                platformPointing = null;
                platformDragging = null;
            }
            spawnColliderQuad.gameObject.SetActive(true);

            if (Input.GetMouseButtonDown(0) && platformPointing != null)
            {
                canClip = true;
                platformDragging = platformPointing;
                if (platformDragging.hasAPlatform())
                {
                    //empeche de bouger
                    platformDragging = null;
                }
            }
            else if (Input.GetMouseButtonDown(1) && platformPointing != null)
            {
                canClip = false;
                platformDragging = platformPointing;
                if (!platformDragging.hasAPlatform())
                {
                    //empeche de bouger
                    platformDragging = null;
                }
            }

            else if (Input.GetMouseButtonUp(0))
            {

                if ((Time.time - doubleLeftClickStart) < timeDoubleClick && platformPointing==null)
                {
                    this.OnDoubleClick();
                    doubleLeftClickStart = -1;
                }
                else
                {
                    doubleLeftClickStart = Time.time;
                }
                
                platformDragging = null;
                
            }

            else if (this.activeLeftPinch == true && this.activeRightPinch == true && this.objectInCreation == false)
            {
                Debug.Log("Hello 2 pinch !");
                this.objectInCreation = true;
                this.OnDoubleClick();
            }
            else if(this.activeLeftPinch == false || this.activeRightPinch == false)
            {
                this.objectInCreation = false;
            }

            else if (Input.GetMouseButtonUp(1) && platformPointing!=null)
            {
                //clic et doule clic right 

                if ((Time.time - doubleRightClickStart) < timeDoubleClick)
                {
                    //double clickRight

                    platformPointing.unactive();
                    doubleRightClickStart = -1;
                }
                else
                {
                    platformPointing.unclip();
                    doubleRightClickStart = Time.time;
                }

                platformDragging = null;
                platformPointing = null;
            }

            if (platformDragging!=null)
            {
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit info;

                if (spawnColliderQuad.Raycast(ray2, out info, 1000))
                {
                    if (info.point.y > 0)
                    {
                        platformDragging.transform.position = info.point;
                        if(canClip)
                        {
                            if (DetectNearestPieceAndAttach(platformDragging))
                            {
                                platformDragging = null;
                            }
                        }
                    }
                }
            }
        }
        

    }

    public PlatformScript findPlatformScriptByGO(GameObject go)
    {
        for(int i=0;i< nbMaxPlatform; i++)
        {
            if(allPlatform[i].gameObject.activeInHierarchy)
            {
                if (allPlatform[i].gameObject == go)
                    return allPlatform[i];
            }
        }
        return null;
    }

    public PlatformScript findPlatformScriptByCubeGO(GameObject go)
    {
        for (int i = 0; i < nbMaxPlatform; i++)
        {
            if (allPlatform[i].gameObject.activeInHierarchy)
            {
                if (allPlatform[i].getCube() == go)
                    return allPlatform[i];
            }
        }
        return null;
    }

    public PlatformScript getPlatform()
    {
        for (int i = 0; i < nbMaxPlatform; i++)
        {
            if (!allPlatform[i].gameObject.activeInHierarchy)
            {
                allPlatform[i].gameObject.SetActive(true);
                return allPlatform[i];
            }
        }
        return null;
    }

    public void LeftPinchActivate()
    {
        Debug.Log("Activate Left Pinch");
        this.activeLeftPinch = true;
    }

    public void LeftPinchDeactivate()
    {
        Debug.Log("DeActivate Left Pinch");
        this.activeLeftPinch = false;
    }

    public void RightPinchActivate()
    {
        Debug.Log("Activate Right Pinch");
        this.activeRightPinch = true;
    }

    public void RightPinchDeactivate()
    {
        Debug.Log("DeActivate Right Pinch");
        this.activeRightPinch = false;
    }

    public void OnDoubleClick()
    {
        var p = this.getPlatform();
        if(p!=null)
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //RaycastHit info;

            //if (spawnColliderQuad.Raycast(ray, out info, 1000))
            //{
                // get the hit point:
              //  if(info.point.y<0)
                //{
                    p.gameObject.SetActive(true);
                    //return;
                //}

                    
                //p.transform.position = averageIndex;
                platformSpawning = p;
            //}
        }
    }


    float RetrieveDistBetweenPiece(PlatformScript pieceOne, PlatformScript pieceTwo, out bool result)
    {
        result = true;
        float d1 = Vector3.Distance(pieceTwo.getGoInMarker().transform.position, pieceOne.getGoOutMarker().transform.position);
        float d2 = Vector3.Distance(pieceOne.getGoInMarker().transform.position, pieceTwo.getGoOutMarker().transform.position);
        /*if (!pieceTwo.hasPlatformOut() && pieceTwo.hasPlatformIn())
        {
            Debug.Log("mindist1");
            result = false;
            return d1;
        }
        else if (pieceTwo.hasPlatformOut() && !pieceTwo.hasPlatformIn())
        {
            Debug.Log("mindist2");
            result = true;
            return d2;
        }*/

        if (d1 < d2)
            return d1;    
        result = false;
        return d2;
    }

    public bool DetectNearestPieceAndAttach(PlatformScript platform)
    {
        PlatformScript nearestPlatform = null;
        float nearest = 10000000f;
        bool result = true;

        for (int i = 0; i < nbMaxPlatform; i++)
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
    
}
