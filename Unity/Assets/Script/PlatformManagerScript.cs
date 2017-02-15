using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManagerScript : MonoBehaviour {

    [SerializeField]
    PieceScript prefab;

    List<PieceScript> allPieces = new List<PieceScript>();

    [SerializeField]
    float timeDoubleClick = .2f;

    [SerializeField]
    int nbMaxPieces = 100;

    [SerializeField]
    Camera worlCamera;

    [SerializeField]
    MeshCollider spawnColliderQuad;

    float doubleClickStart = 0;

    PieceScript pieceSpawning = null;

    // Use this for initialization
    void Start () {
        Debug.Log("start");

        for(int i=0;i<this.nbMaxPieces;i++)
        {
            var p = (PieceScript)Instantiate(prefab);
            p.gameObject.SetActive(false);
            p.transform.parent = transform;
            allPieces.Add(p);
        }
        
    }
	
	// Update is called once per frame
	void Update () {

        if(pieceSpawning!=null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit info;

            if (spawnColliderQuad.Raycast(ray, out info, 1000))
            {
                pieceSpawning.transform.localScale = new Vector3(Mathf.Abs(info.point.x - pieceSpawning.transform.position.x)*2, pieceSpawning.transform.localScale.y, pieceSpawning.transform.localScale.z);
            }

            if (Input.GetMouseButtonUp(0))
            {
                pieceSpawning = null;
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                if ((Time.time - doubleClickStart) < timeDoubleClick)
                {
                    this.OnDoubleClick();
                    doubleClickStart = -1;
                }
                else
                {
                    Debug.Log("click");
                    doubleClickStart = Time.time;
                }
            }
        }
        

    }

    public PieceScript getPlatform()
    {
        for (int i = 0; i < nbMaxPieces; i++)
        {
            if (!allPieces[i].gameObject.activeInHierarchy)
            {
                allPieces[i].gameObject.SetActive(true);
                return allPieces[i];
            }
        }
        return null;
    }


    void OnDoubleClick()
    {
        Debug.Log("Double Clicked!");
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
                pieceSpawning = p;
            }
        }
    }
}
