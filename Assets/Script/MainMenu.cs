using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayStoryMode() {
        SceneManager.LoadScene(1);
    }
    public void PlayArcadeMode(){
        SceneManager.LoadScene(2);
    }

    public void PlayInfiniteMode(){
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        //Debug.Log("Quit!");
        Application.Quit();
    }
}
