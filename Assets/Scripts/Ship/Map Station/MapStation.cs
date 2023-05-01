using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStation : MonoBehaviour, IInteractStation
{
    public GameObject tutorialText;
    MeshRenderer mapTutorial;
    public bool RetainControlOnSwap { get { return false; } }

    void Start()
    {
        mapTutorial = GameObject.Find("MapTutorial").GetComponent<MeshRenderer>();
    }

    public void SetInteracting(bool isInteracting, RatControllerBehavior rat)
    {
        ZoomCamera(isInteracting);
    }

    public void ZoomCamera(bool zoom)
    {
        mapTutorial.enabled = zoom;
        if (zoom)
        {
            Camera.main.transform.position -= new Vector3(3.7f, 2.0f);
            Camera.main.orthographicSize = 2.5f;
        }
        else
        {
            Camera.main.transform.position += new Vector3(3.7f, 2.0f);
            Camera.main.orthographicSize = 5f;
        }
    }

    public bool CanInteract()
    {
        return true;
    }
}
