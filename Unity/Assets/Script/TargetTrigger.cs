using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger : MonoBehaviour
{
    [SerializeField]
    GameManager _gameManager;

    [SerializeField]
    PlatformManagerScript _platformManager;

    NopeZoneDetection[] _nopes;
    //bool _nopeCrossed = false;

    bool _budgetOver = false;

    private bool _hasWon = false;

    void Start()
    {
        _nopes = FindObjectsOfType<NopeZoneDetection>();
    }

    void OnTriggerEnter(Collider ball)
    {
        for(int i = 0; i < _nopes.Length; ++i)
        {
            if(_nopes[i].IsCrossed())
            {
                _hasWon = false;
                _gameManager.Lose();
                return;
            }
        }

        //_budgetOver = _platformManager.IsBudgetOver();
        if (_budgetOver)
        {
            _hasWon = false;
            _gameManager.Lose();
            return;
        }

        _gameManager.Win();
        _hasWon = true;
    }
    
    public bool GetHasWon()
    {
        return _hasWon;
    }
}
