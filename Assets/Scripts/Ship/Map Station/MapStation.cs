using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStation : MonoBehaviour, IInteractStation
{
    Vector3 ogPosition;
    float ogSize;

    public bool RetainControlOnSwap { get { return false; } }

    public void SetInteracting(bool isInteracting, RatControllerBehavior rat)
    {
        ogPosition = Camera.main.transform.position;
        ogSize = Camera.main.orthographicSize;
        ZoomCamera(isInteracting);
    }

    public void ZoomCamera(bool zoom)
    {
        if (zoom)
        {
            Camera.main.transform.position -= new Vector3(4.4f, 1.0f);
            Camera.main.orthographicSize -= 3.5f;
        }
        else
        {
            Camera.main.transform.position += new Vector3(4.4f, 1.0f);
            Camera.main.orthographicSize += 3.5f;
        }
    }

    public bool CanInteract()
    {
        return true;
    }
}
