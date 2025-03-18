using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime=1f;
    
     void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            CharacterSelectBack();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            StageSelectBack();
        }
        else
        {
            return;
        }
    }
    public void CharacterSelectBack() //If Pressed Go Back to Main Menu or Start Screen Scene
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            MainMenu();
        }
    }

    public void StageSelectBack() //If Pressed Go Back to Character Selection Scene
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CharacterSelection();
        }
    }

    public void clickStart() //If the button Start is clicked
    {
            LoadNextScene(); //Load Next Scene Function
    }
    public void forestStage()
    {
        Time.timeScale = 1f; // Resume the time
        StartCoroutine(LoadScene(3));//Load to Forest Stage Scene
    }
    public void plainsStage()
    {
        Time.timeScale = 1f; // Resume the time
        StartCoroutine(LoadScene(4));//Load to Plains Stage Scene
    }
    public void CharacterSelection()
    {
        StartCoroutine(LoadScene(1)); //If Pressed Go Back to Character Selection Scene
    }
    public void MainMenu()
    { 
        Time.timeScale = 1f; // Resume the time
        StartCoroutine(LoadScene(0));//Load to Start Screen Scene
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));//Load the Scene base on its Order
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(sceneIndex);

        SceneManager.LoadScene(sceneIndex);

    }

}
