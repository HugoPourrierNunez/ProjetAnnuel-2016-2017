using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    private bool hasWon = false;

    void OnTriggerEnter(Collider ball)
    {
        gameManager.Win();
        hasWon = true;
    }
    
    public bool getHasWon()
    {
        return hasWon;
    }
}
