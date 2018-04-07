using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    [SerializeField]
    private GameObject canvasConfirmation;

    [SerializeField]
    VRPlatformManagerScript platformManager;

    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    Button btnPlayCanvasEdit;

    [SerializeField]
    Button btnMenuEditCanvasEdit;

    bool confirmationCanvasPositionSet = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("CanvasManager platform ? : " + platformManager.isTherePlatform);
        if(!platformManager.isTherePlatform)
        {
            //var colorsBtn = btnPlayCanvasEdit.colors;
            //colorsBtn.disabledColor = new Color(221, 221, 221, 128);
            //btnPlayCanvasEdit.colors = btnMenuEditCanvasEdit.colors;
            btnPlayCanvasEdit.interactable = false;
        }
        else
        {
            if(!btnPlayCanvasEdit.interactable)
                btnPlayCanvasEdit.interactable = true;
        }
    }

    public void ShowConfirmationCanvas()
    {
        Debug.Log("Show " + canvasConfirmation.active);

        if (!confirmationCanvasPositionSet)
        {
            canvasConfirmation.transform.position = new Vector3(playerCamera.transform.position.x + canvasConfirmation.transform.position.x,
            playerCamera.transform.position.y + canvasConfirmation.transform.position.y,
            playerCamera.transform.position.z + canvasConfirmation.transform.position.z);
            confirmationCanvasPositionSet = true;
        }

        canvasConfirmation.SetActive(true);
    }

    public void HideConfirmationCanvas()
    {
        Debug.Log("Hide " + canvasConfirmation.active);
        if (canvasConfirmation.active)
        {
            canvasConfirmation.SetActive(false);
        }
    }
}
