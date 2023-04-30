using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    Text time;
    void Awake()
    {
        time = GameObject.Find("Game Over Time").GetComponent<Text>();
        time.text = GameValues.timeInAir.ToString() + " Seconds";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinScreen()
    {

    }

    public void LoseScreen()
    {

    }
}
