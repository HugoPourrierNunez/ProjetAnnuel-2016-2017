using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidBall;

    [SerializeField]
    Transform spawnTransform;

    [SerializeField]
    Transform ballTransform;

    [SerializeField]
    Canvas _canvasVictory;

    [SerializeField]
    Canvas _canvasLose;

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
        rigidBall.isKinematic = true;
        _gamestate = 0;
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
        rigidBall.isKinematic = false;
        _gamestate = 3;
    }

    public void Edit()
    {
        if (_gamestate == 4)
        {
            _canvasLose.gameObject.SetActive(false);
            _canvasVictory.gameObject.SetActive(false);
        }
        rigidBall.isKinematic = true;
        ballTransform.position = spawnTransform.position;
        ballTransform.rotation.Set(0.0f, 0.0f, 0.0f, 0.0f);
        _gamestate = 2;
    }

    public int getGamestate()
    {
        return _gamestate;
    }

    public void Redo()
    {
        if (_gamestate == 4)
        {
            _canvasLose.gameObject.SetActive(false);
            _canvasVictory.gameObject.SetActive(false);
        }
    }

    public void GoToMenu()
    {
        if (_gamestate == 4)
        {
            _canvasLose.gameObject.SetActive(false);
            _canvasVictory.gameObject.SetActive(false);
        }
        _gamestate = 0;
    }

    public void NextLevel()
    {
        StartLevel(++_level);
    }

    public void StartLevel(int level)
    {
        Debug.Log("Starting level" + _level);
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
