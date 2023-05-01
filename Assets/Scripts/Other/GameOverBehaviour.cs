using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverBehaviour : MonoBehaviour
{
    public Text ResultText;
    public GameObject VictoryRoot;
    public GameObject GameOverRoot;

    void Start()
    {
        //GameValues.FakeForGameOver();
        
        TimeSpan timeSpan = TimeSpan.FromSeconds(GameValues.TimeInAir);

        var gameOverString = GameValues.IsWin ? "ye spread the plague in " : "yer ship was sank after ";

        gameOverString += timeSpan.ToString("mm':'ss");

        ResultText.text = gameOverString;

        if (GameValues.IsWin)
        {
            VictoryRoot.SetActive(true);
        }
        else
        {
            GameOverRoot.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
