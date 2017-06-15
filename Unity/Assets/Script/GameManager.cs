using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Rigidbody[] rigidBall;

    [SerializeField]
    Transform[] spawnTransform;

    [SerializeField]
    Transform[] ballTransform;

    [SerializeField]
    Canvas _canvasVictory;

    [SerializeField]
    Canvas _canvasLose;

    [SerializeField]
    LevelLock _levelLock;

    [SerializeField]
    LevelScript[] _levels;

    [SerializeField]
    HandMenuManager handMenuManager;

    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    Vector3 levelPositionRelCamera;

    [SerializeField]
    Vector3 canvasPositionRelCamera;

    [SerializeField]
    VRPlatformManagerScript vRPlatformManagerScript;

    private int levelInProgress = -1;

    //Gamestate : 0 (menu), 1 (levels), 2 (edit), 3 (play), 4 (end)
    private int _gamestate;

    private int _level;

    void Start()
    {
        var prevLvl = PlayerPrefs.GetInt("max_level", -1);
        if (prevLvl == -1)
        {
            PlayerPrefs.SetInt("max_level", 1);
        }        
        _gamestate = 0;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SelectLevel()
    {
        _gamestate = 1;
        handMenuManager.activateContextualHandMenu(true);
    }

    public int GetLevel()
    {
        return _level;
    }

    public void Win()
    {

        Save();


        _levels[levelInProgress].gameObject.SetActive(false);

        vRPlatformManagerScript.unactiveAllPlatform();

        _canvasVictory.transform.position = new Vector3(playerCamera.transform.position.x + canvasPositionRelCamera.x,
                    playerCamera.transform.position.y + canvasPositionRelCamera.y,
                    playerCamera.transform.position.z + canvasPositionRelCamera.z);
        _canvasVictory.gameObject.SetActive(true);
        _gamestate = 4;
        if(_level < _levels.Length)
        {
            _levelLock.UnlockLevel(_level);
        }
    }

    public void Lose()
    {
        _levels[levelInProgress].gameObject.SetActive(false);
        vRPlatformManagerScript.unactiveAllPlatform();

        _canvasLose.transform.position = new Vector3(playerCamera.transform.position.x + canvasPositionRelCamera.x,
                       playerCamera.transform.position.y + canvasPositionRelCamera.y,
                       playerCamera.transform.position.z + canvasPositionRelCamera.z);
        _canvasLose.gameObject.SetActive(true);
        _gamestate = 4;
    }

    public void Play()
    {
        if (_gamestate == 4)
        {
            _canvasLose.gameObject.SetActive(false);
            _canvasVictory.gameObject.SetActive(false);
        }
        rigidBall[_level].isKinematic = false;
        _gamestate = 3;
        handMenuManager.activateContextualHandMenu(true);
    }

    public void Edit()
    {
        if (_gamestate == 4)
        {
            _canvasLose.gameObject.SetActive(false);
            _canvasVictory.gameObject.SetActive(false);
        }
        rigidBall[_level].isKinematic = true;
        ballTransform[_level].position = spawnTransform[_level].position;
        ballTransform[_level].rotation.Set(0.0f, 0.0f, 0.0f, 0.0f);
        _gamestate = 2;
        handMenuManager.activateContextualHandMenu(true);
    }

    public int getGamestate()
    {
        return _gamestate;
    }

    public void Retry()
    {
        //Delete all platforms
        _levels[_level].gameObject.SetActive(false);
        if (_gamestate == 4)
        {
            _canvasLose.gameObject.SetActive(false);
            _canvasVictory.gameObject.SetActive(false);
        }
        StartLevel(_level);
    }

    public void GoToMenu()
    {
        //Delete all platforms
        _levels[_level].gameObject.SetActive(false);
        if (_gamestate == 4)
        {
            _canvasLose.gameObject.SetActive(false);
            _canvasVictory.gameObject.SetActive(false);
        }
        _gamestate = 0;
        handMenuManager.activateContextualHandMenu(true);


        vRPlatformManagerScript.unactiveAllPlatform();
    }

    public void NextLevel()
    {
        //Delete all platforms
        _levels[_level - 1].gameObject.SetActive(false);
        StartLevel(++_level);
    }

    public void StartLevel()
    {
        vRPlatformManagerScript.unactiveAllPlatform();
        _levels[levelInProgress].gameObject.SetActive(true);
        rigidBall[levelInProgress].isKinematic = true;
        ballTransform[levelInProgress].rotation.Set(0.0f, 0.0f, 0.0f, 0.0f);
        ballTransform[levelInProgress].position = spawnTransform[levelInProgress].position;
        _gamestate = 2;
        handMenuManager.activateContextualHandMenu(true);
        reinitLevelPosition();
    }


    public void StartNextLevel()
    {
        _levels[levelInProgress].gameObject.SetActive(false);
        if (levelInProgress < _levels.Length - 1)
            levelInProgress++;


        vRPlatformManagerScript.unactiveAllPlatform();
        vRPlatformManagerScript.setPlatformStart(_levels[levelInProgress].getPlatformStart());
        _levels[levelInProgress].gameObject.SetActive(true);
        rigidBall[levelInProgress].isKinematic = true;
        ballTransform[levelInProgress].rotation.Set(0.0f, 0.0f, 0.0f, 0.0f);
        ballTransform[levelInProgress].position = spawnTransform[levelInProgress].position;
        _gamestate = 2;
        handMenuManager.activateContextualHandMenu(true);
        reinitLevelPosition();
    }


    public void HideVictoryCanvas()
    {
        _canvasVictory.gameObject.SetActive(false);
    }

    public void HideLooseCanvas()
    {
        _canvasLose.gameObject.SetActive(false);
    }

    public void StartLevel(int level)
    {
        levelInProgress = level-1;
        vRPlatformManagerScript.setPlatformStart(_levels[level - 1].getPlatformStart());
        _levels[level - 1].gameObject.SetActive(true);
        rigidBall[level - 1].isKinematic = true;
        ballTransform[level - 1].rotation.Set(0.0f, 0.0f, 0.0f, 0.0f);
        ballTransform[level - 1].position = spawnTransform[level - 1].position;
        _gamestate = 2;
        handMenuManager.activateContextualHandMenu(true);
        reinitLevelPosition();
    }

    public void Save()
    {
        var prevLvl = PlayerPrefs.GetInt("max_level", -1);
        if(_level > prevLvl)
        {
            PlayerPrefs.SetInt("max_level", _level);
        }
    }

    public void reinitLevelPosition()
    {
        _levels[levelInProgress].transform.position = new Vector3(playerCamera.transform.position.x + levelPositionRelCamera.x,
                    playerCamera.transform.position.y + levelPositionRelCamera.y,
                    playerCamera.transform.position.z + levelPositionRelCamera.z);
    }
}
