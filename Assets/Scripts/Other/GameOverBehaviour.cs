using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverBehaviour : MonoBehaviour
{
    Text gameOverText;

    void Awake()
    {
        gameOverText = GameObject.Find("Game Over Text").GetComponent<Text>();
        TimeSpan timeSpan = TimeSpan.FromSeconds(GameValues.TimeInAir);

        var gameOverString = GameValues.IsWin ? "You spread the plague in " : "You kept the ship alive for ";

        gameOverString += timeSpan.ToString("mm':'ss") + "!!";

        gameOverText.text = gameOverString;
    }

    public void WinScreen()
    {

    }

    public void LoseScreen()
    {

    }
}
