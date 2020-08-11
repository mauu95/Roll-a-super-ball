using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    private const string PICKUP_LEVEL_KEY = "PickUpLevel";
    public enum LevelState
    {
        LOCKED,
        UNLOCKED,
        COMPLETED
    }

    const int LEVEL_COUNT = 7;
    public int currentLevel { get; set; }


    public GameObject Player;
    public bool IsPause;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RealoadLevel();
        if (Player && Player.transform.position.y < 0)
            RealoadLevel();
        if (Input.GetKeyDown(KeyCode.G))
        {
            for(int i = 0; i< LEVEL_COUNT; i++)
            {
                int x = PlayerPrefs.GetInt(PICKUP_LEVEL_KEY + i, -1);
                print(i + ": " + x);
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            for (int i = 0; i < LEVEL_COUNT; i++)
            {
                PlayerPrefs.SetInt(PICKUP_LEVEL_KEY + i, -1);
            }

            PlayerPrefs.SetInt(PICKUP_LEVEL_KEY + 0, 0);
        }


    }

    public void CursorOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CursorOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RealoadLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel(int n)
    {
        currentLevel = n;

        if (n == 0)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(2);

    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TogglePause()
    {
        IsPause = !IsPause;
        if (IsPause)
        {
            Time.timeScale = 0;
            CursorOn();
        }
        else
        {
            Time.timeScale = 1;
            CursorOff();
        }
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void PickedAPickUp(int value)
    {
        int old = PlayerPrefs.GetInt(PICKUP_LEVEL_KEY + currentLevel, -1);

        if (old < value)
            PlayerPrefs.SetInt(PICKUP_LEVEL_KEY + currentLevel, value);  
    }

    public int getPickUpsValue(int level)
    {
        return PlayerPrefs.GetInt(PICKUP_LEVEL_KEY + level, -1);
    }

    public void Unlock(int level)
    {
        int old = PlayerPrefs.GetInt(PICKUP_LEVEL_KEY + level, -1);

        if (old < 0)
            PlayerPrefs.SetInt(PICKUP_LEVEL_KEY + level, 0);
    }

}
