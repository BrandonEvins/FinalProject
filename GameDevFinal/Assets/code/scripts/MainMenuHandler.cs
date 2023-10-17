using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("GamePlay");
    }

    public void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();//This does not work in your editor

    }

    public void HowToPlay(){
        SceneManager.LoadScene("HowTo");
    }

    public void BackMainMenu(){
        SceneManager.LoadScene("SampleScene");
    }
}