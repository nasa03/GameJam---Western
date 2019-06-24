using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    float timeTillStarts;
    public static bool isPaused;
    public GameObject menu;

	// Use this for initialization
	void Start () {
        PauseGame(true);
	
	}
	
	// Update is called once per frame
	void Update () {
        if(isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame(true);
            }
            else
            {
                PauseGame(false);
            }
        }
        
    }

    public void PauseGame(bool value)
    {
        if(value)
        {
            isPaused = true;
            menu.SetActive(true);
        }
        else
        {
            isPaused = false;
            menu.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
