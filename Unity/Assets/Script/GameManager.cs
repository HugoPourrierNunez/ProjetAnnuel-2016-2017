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
    GameObject[] _levels;

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
    }

    public int GetLevel()
    {
        return _level;
    }

    public void Win()
    {
        Save();
        _canvasVictory.gameObject.SetActive(true);
        _gamestate = 4;
        if(_level < _levels.Length)
        {
            _levelLock.UnlockLevel(_level);
        }
    }

    public void Lose()
    {
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
        rigidBall[_level - 1].isKinematic = false;
        _gamestate = 3;
    }

    public void Edit()
    {
        if (_gamestate == 4)
        {
            _canvasLose.gameObject.SetActive(false);
            _canvasVictory.gameObject.SetActive(false);
        }
        rigidBall[_level - 1].isKinematic = true;
        ballTransform[_level - 1].position = spawnTransform[_level - 1].position;
        ballTransform[_level - 1].rotation.Set(0.0f, 0.0f, 0.0f, 0.0f);
        _gamestate = 2;
    }

    public int getGamestate()
    {
        return _gamestate;
    }

    public void Retry()
    {
        //Delete all platforms
        _levels[_level - 1].SetActive(false);
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
        _levels[_level - 1].SetActive(false);
        if (_gamestate == 4)
        {
            _canvasLose.gameObject.SetActive(false);
            _canvasVictory.gameObject.SetActive(false);
        }
        _gamestate = 0;
    }

    public void NextLevel()
    {
        //Delete all platforms
        _levels[_level - 1].SetActive(false);
        StartLevel(++_level);
    }

    public void StartLevel(int level)
    {
        _levels[level - 1].SetActive(true);
        rigidBall[_level - 1].isKinematic = true;
        ballTransform[_level - 1].rotation.Set(0.0f, 0.0f, 0.0f, 0.0f);
        ballTransform[_level - 1].position = spawnTransform[_level - 1].position;
    }

    public void Save()
    {
        var prevLvl = PlayerPrefs.GetInt("max_level", -1);
        if(_level > prevLvl)
        {
            PlayerPrefs.SetInt("max_level", _level);
        }
    }
}
