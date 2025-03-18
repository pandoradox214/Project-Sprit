using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject EndGamePanel;
    public static bool isPaused = false;
    public static float pauseTime; 
    public TextMeshProUGUI winText;
    public GameObject Player1, Player2;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        EndGamePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            if (isPaused)//If the Game was paused Call ResumeGame() function
            {
                ResumeGame();
            }
            else //Else Call PauseGame() function
            {
                PauseGame();
            }
        }
        GameEnd();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; //Pause The game
        pauseMenu.SetActive(true); //Shows the Pause Menu Panel
        isPaused = true; //Sets isPaused to true
        pauseTime = Time.time;  //Records the time that the game was paused
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f; //Resumes the game
        pauseMenu.SetActive(false); //Hides the Pause Menu Panel
        isPaused = false;//Sets isPaused to true
    }

    public void resetPause()//Reset isPaused variable when the Gameplay scene is disabled
    {
        isPaused = false;
    }
    public void QuitGame() 
    {
        Application.Quit();
    }
    public void GameEnd()
    {//If One of the Player isDead Show the EndGamePanel 
        if (Player1 == null || Player2 == null)
        {
            Time.timeScale = 1f;//Resume The game
            EndGamePanel.SetActive(true);//Show End Game Panel
        }
    }
    public void RestartGame() //Only shows on EndGamePanel
    {
        resetPause();
        EndGamePanel.SetActive(false);//Hide End Game Panel
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//Loads the same game scene
    }

}
