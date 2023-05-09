using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("SampleScene");
        }
    }
}
