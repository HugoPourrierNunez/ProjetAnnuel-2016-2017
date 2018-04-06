using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuManager : MonoBehaviour {

    [SerializeField]
    Canvas handMenu;

    [SerializeField]
    Canvas handlevelMenu;

    [SerializeField]
    Canvas handEdit;

    [SerializeField]
    Canvas handPlay;

    [SerializeField]
    Canvas handReinit;

    [SerializeField]
    GameManager gameManager;

    public void activateContextualHandMenu(bool b)
    {
        Debug.Log("activateContextualHandMenu");
        handMenu.gameObject.SetActive(false);
        handlevelMenu.gameObject.SetActive(false);
        handEdit.gameObject.SetActive(false);
        handPlay.gameObject.SetActive(false);
        handReinit.gameObject.SetActive(false);
        if (b)
        {
            //Gamestate: 0(menu), 1(levels), 2(edit), 3(play), 4(end)
            int state = gameManager.getGamestate();
            UIManagerScript.instance.reinitHands();
            Debug.Log("getGamestate=" +state);
            if (state==0)
            {
                handMenu.gameObject.SetActive(true);
            }
            else if (state == 1)
            {
                handMenu.gameObject.SetActive(true);
                handlevelMenu.gameObject.SetActive(true);
            }
            else if (state == 2)
            {
                handEdit.gameObject.SetActive(true);
                handReinit.gameObject.SetActive(true);
            }
            else if (state == 3)
            {
                handPlay.gameObject.SetActive(true);
                handReinit.gameObject.SetActive(true);
            }
        }
    }

}
