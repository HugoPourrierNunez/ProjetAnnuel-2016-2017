using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuManager : MonoBehaviour {

    [SerializeField]
    Canvas handMenu;

    [SerializeField]
    Canvas handlevelMenu;

    [SerializeField]
    Canvas handPlay;

    [SerializeField]
    Canvas handPause;

    [SerializeField]
    GameManager gameManager;

    public void activateContextualHandMenu(bool b)
    {
        if(b)
        {

        }
        else
        {
            handMenu.gameObject.SetActive(false);
            handlevelMenu.gameObject.SetActive(false);
            handPlay.gameObject.SetActive(false);
            handPause.gameObject.SetActive(false);
        }
    }

}
