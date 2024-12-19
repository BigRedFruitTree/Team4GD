using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public PlayerMovement1 playerData;
    
    public Slider healthBar;
    public GameObject healthBarStuff;
    public GameObject pausemenu;
    public GameObject gameoverScreen;
    public GameObject endScreen;
    public bool finished = false;
    public bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0)
        {
           pausemenu.SetActive(false);
           gameoverScreen.SetActive(false);
           if(SceneManager.GetActiveScene().buildIndex > 0)
           {
            
             Cursor.lockState = CursorLockMode.Confined;
             Cursor.visible = false;
           }
        }

        if(SceneManager.GetActiveScene().buildIndex > 3) 
        {
            if(playerData.endGameActive == true) 
            {
                 Cursor.lockState = CursorLockMode.None;
                 Cursor.visible = (true);
                 healthBarStuff.SetActive(false);
            }
        }
    }

    //this note
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            healthBar.value = playerData.health;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    pausemenu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = (true);
                    isPaused = true;

                }
                else
                {
                    Resume();
                }

                if (reset && isPaused)
                {
                    RestartLevel();
                }
            }
            if(playerData.health <= 0)
            {
                
                gameoverScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = (true);
                isPaused = true;
                reset = true;
            }

        }
    }
    public void Resume()
    {
        pausemenu.SetActive(false);

        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

   public void LoadLevel(int sceneID)
   {
        SceneManager.LoadScene(sceneID);
   }

     public void RestartLevel()
     {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;


    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        
    }

   

}