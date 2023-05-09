using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverBehaviour : MonoBehaviour
{
    public Text ResultText;
    public Text ResultTime;
    public Text GameOverTimeInMilliseconds;
    public GameObject VictoryRoot;
    public GameObject GameOverRoot;
    public GameObject LeaderboardUI;
    public GameObject NameInputUI;

    void Start()
    {
        //GameValues.FakeForVictory();
        
        TimeSpan timeSpan = TimeSpan.FromSeconds(GameValues.TimeInAir);
        int timeSpanMilliseconds = (int)Math.Round(GameValues.TimeInAir * 1000);

        var gameOverString = GameValues.IsWin ? "Ye spread da plague in " : "Yer ship crashed after ";

        var gameOverTime = timeSpan.ToString("mm':'ss'.'fff");

        ResultText.text = gameOverString;
        ResultTime.text = gameOverTime;
        GameOverTimeInMilliseconds.text = timeSpanMilliseconds.ToString();

        if (GameValues.IsWin)
        {
            VictoryRoot.SetActive(true);
            LeaderboardUI.SetActive(true);
            NameInputUI.SetActive(true);
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
