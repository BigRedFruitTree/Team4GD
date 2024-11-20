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
    public Image healthBar;
    public GameObject pausemenu;
    public bool finished = false;

    // Start is called before the first frame update
    void Start()
    {
        pausemenu.SetActive(false);

    }

    //this note
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
                {
                    Resume();
                }

            }
        }
        void Resume()
        {
            pausemenu.SetActive(false);

            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;

            isPaused = false;
        }

        void QuitGame()
        {
            Application.Quit();
        }

        void LoadLevel(int sceneID)
        {
            SceneManager.LoadScene(sceneID);
        }

        void RestartLevel()
        {
            LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.gameObject.tag == "DOOR")
            {
                LoadLevel(2);
            }
            if (collision.gameObject.tag == "HIVE")
            {
                LoadLevel(4);
            }
        }
    }
}