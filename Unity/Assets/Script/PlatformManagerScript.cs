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
    int nbMaxPlatform = 10;

    [SerializeField]
    Camera worlCamera;

    [SerializeField]
    MeshCollider spawnColliderQuad;

    [SerializeField]
    public float distanceClose=0.4f;

    float doubleClickStart = 0;

    bool buttonDown=false;

    PlatformScript platformSpawning = null;

    PlatformScript platformDragging = null;

    // Use this for initialization
    void Start () {
        Debug.Log("start");

        for(int i=0;i<this.nbMaxPlatform; i++)
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
                platformSpawning.getCube().transform.localScale = new Vector3(Mathf.Abs(info.point.x - platformSpawning.transform.position.x)*2, platformSpawning.transform.localScale.y, platformSpawning.transform.localScale.z);

                /*float diffX = Mathf.Abs(info.point.x - platformSpawning.transform.position.x)/2;
                float diffY = Mathf.Abs(info.point.y - platformSpawning.transform.position.y)/2;
                
                platformSpawning.transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Rad2Deg*Mathf.Asin(diffY / diffX));*/

                platformSpawning.positionInOut();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if(platformSpawning!=null)
                {
                    platformSpawning.positionInOut();
                    platformSpawning = null;
                }
            }
        }
        else
        {

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.tag == "Piece")
                    {
                        platformDragging = findPlatformScriptByCubeGO(hit.collider.gameObject);
                        if (platformDragging.isClipped())
                            platformDragging = null;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {

                if ((Time.time - doubleClickStart) < timeDoubleClick)
                {
                    this.OnDoubleClick();
                    doubleClickStart = -1;
                }
                else
                {
                    doubleClickStart = Time.time;
                }
                
                platformDragging = null;
                
            }

            if (platformDragging!=null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit info;

                if (spawnColliderQuad.Raycast(ray, out info, 1000))
                {
                    if (info.point.y > 0)
                    {
                        platformDragging.transform.position = info.point;
                        if (DetectNearestPieceAndAttach(platformDragging))
                        {
                            platformDragging.setClipped(true);
                            platformDragging = null;
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


    void OnDoubleClick()
    {
        var p = this.getPlatform();
        if(p!=null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit info;

            if (spawnColliderQuad.Raycast(ray, out info, 1000))
            {
                // get the hit point:
                if(info.point.y<0)
                {
                    p.gameObject.SetActive(false);
                    return;
                }

                p.transform.position = info.point;
                platformSpawning = p;
            }
        }
    }


    float RetrieveDistBetweenPiece(PlatformScript pieceOne, PlatformScript pieceTwo)
    {
        GameObject inPieceTwo = pieceTwo.getGoIn();
        GameObject outPieceOne = pieceOne.getGoOut();
        return Mathf.Abs((float)Mathf.Sqrt(Mathf.Pow(outPieceOne.transform.position.x - inPieceTwo.transform.position.x, 2) + Mathf.Pow(outPieceOne.transform.position.y - inPieceTwo.transform.position.y, 2) + Mathf.Pow(outPieceOne.transform.position.z - inPieceTwo.transform.position.z, 2)));

    }

    public bool DetectNearestPieceAndAttach(PlatformScript platform)
    {
        PlatformScript nearestPlatform = null;
        float nearest = 10000000f;

        for (int i = 0; i < nbMaxPlatform; i++)
        {
            if (allPlatform[i].gameObject.activeInHierarchy && allPlatform[i]!= platform)
            {
                float currentDist = RetrieveDistBetweenPiece(platform, allPlatform[i]);

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

                GameObject inGameObject = nearestPlatform.getGoIn();
                GameObject outGameObject = platform.getGoOut();

                Vector3 vecPos = outGameObject.transform.localPosition;

                outGameObject.transform.position = inGameObject.transform.position;
                inGameObject.transform.localPosition = outGameObject.transform.position - vecPos;
                return true;
            }
        }
        return false;
    }
    
}
