using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    const int LEVEL_COUNT = 5;
    public int LevelPlaying { get; set; }
    public GameObject Player;
    public bool IsPause;


    private GameState gameState;

    private void Start()
    {
        gameState = new GameState(LEVEL_COUNT);
        LevelPlaying = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RealoadLevel();
        if (Player && Player.transform.position.y < 0)
            RealoadLevel();
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

    public void PickedAPickUp(int count)
    {
        gameState.UpdateLevel(LevelPlaying, count);
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



    class GameState {
    private const string PICKUP_TUTORIAL_KEY = "PickUpTutorial";
    private const string PICKUP_LEVEL_KEY = "PickUpLevel";
    public int pickupTutorial;
    public int[] pickupLevels;

    private int levels;

    public GameState(int levels) {
        pickupTutorial = PlayerPrefs.GetInt(PICKUP_TUTORIAL_KEY, 0);
        pickupLevels = new int[levels];
        for (int i = 0; i < levels; i++) {
            pickupLevels[i] = PlayerPrefs.GetInt(PICKUP_LEVEL_KEY + (i + 1), 0);
        }
        this.levels = levels;
    }

    public void UpdateLevel(int level, int value) {
        if (level == 0) {
            pickupTutorial = Mathf.Max(pickupTutorial, value);
            CompareAndSave(pickupTutorial, value, PICKUP_TUTORIAL_KEY);
        } else {
            pickupLevels[level - 1] = Mathf.Max(pickupLevels[level - 1], value);
            CompareAndSave(pickupLevels[level - 1], value, PICKUP_LEVEL_KEY);
        }
    }


    private void CompareAndSave(int toSave, int toCompare, string key) {
        if (toSave == toCompare) {
            PlayerPrefs.SetInt(key, toSave);
        }
    }

    }
}
