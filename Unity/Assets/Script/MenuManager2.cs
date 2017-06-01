using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager2 : MonoBehaviour
{
    [SerializeField]
    Button playBtn;

    [SerializeField]
    Button exitBtn;

    [SerializeField]
    Animator anim;

    public void Exit()
    {
        Application.Quit();
    }

    public void LaunchGame()
    {
        //playBtn.gameObject.SetActive(false);
        //exitBtn.gameObject.SetActive(false);
        Debug.Log("LaunchGame");
        //anim.SetBool("chooseLvl", true);
    }

    public void LaunchLevel(int lvl)
    {
        //SceneManager.LoadScene("Level" + lvl);
        Debug.Log("LaunchLevel");
    }

    public void Return()
    {
        playBtn.gameObject.SetActive(true);
        exitBtn.gameObject.SetActive(true);

        anim.SetBool("chooseLvl", false);
    }

}
