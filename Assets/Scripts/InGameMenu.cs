using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool Paused = false;
    public GameObject panel;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (Paused)
            {
                Resume();
            }
            else if (!Paused)
            {
                PauseMenu();
            }
            
        }

        
    }
   public void Resume()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    void PauseMenu()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }
    public void MenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
   public void ResetButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Paused = false;
       
    }
}