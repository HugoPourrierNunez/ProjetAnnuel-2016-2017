using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TargetTrigger target;

    [SerializeField]
    Animator anim;

    [SerializeField]
    Rigidbody rigidBall;

    [SerializeField]
    Button playButton;

    [SerializeField]
    Button editButton;

    [SerializeField]
    Transform spawnTransform;

    [SerializeField]
    Transform ballTransform;

    //Gamestate : 1 (edit), 2 (play), 3 (win)
    private int gamestate;

    void Start()
    {
        rigidBall.isKinematic = true;
        gamestate = 1;
    }

    public void Win()
    {
        editButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);

        anim.SetBool("playMode", false);
        anim.SetBool("win", true);
        playButton.gameObject.SetActive(false);

        gamestate = 3;
    }

    public void Play()
    {
        playButton.gameObject.SetActive(false);
        editButton.gameObject.SetActive(true);

        rigidBall.isKinematic = false;
        anim.SetBool("editMode", false);
        anim.SetBool("playMode", true);

        gamestate = 2;
    }

    public void Edit()
    {
        editButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);

        if (anim.GetBool("playMode"))
        {
            anim.SetBool("playMode", false);
        }

        rigidBall.isKinematic = true;
        anim.SetBool("editMode", true);

        ballTransform.position = spawnTransform.position;
        ballTransform.rotation.Set(0.0f, 0.0f, 0.0f, 0.0f);

        gamestate = 1;
    }

    public int getGamestate()
    {
        return gamestate;
    }

    public void Redo()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
