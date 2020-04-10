using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if(Player.transform.position.y < 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
