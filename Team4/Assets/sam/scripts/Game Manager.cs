using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public PlayerMovement playerData;
    public Image healthBar;
    public GameObject pausemenu;
    public bool finished = false;

    // Start is called before the first frame update
    void Start()
    {
        pausemenu.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    { 
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            healthBar.fillAmount = Mathf.Clamp((float)playerData.health / (float)playerData.maxHealth, 0, 1);
           
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    pausemenu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = (true);
                    Time.timeScale = 0;
                    isPaused = true;

                }
                else
                    Resume();
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
    }

}
