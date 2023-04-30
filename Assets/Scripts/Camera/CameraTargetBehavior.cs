using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CameraTargetBehavior : MonoBehaviour
{
    // There's a timing issue when the game starts that
    // causes the camera to focus on the wrong rat.
    // Ignore trigger activations until this delay
    // has completed so we don't steal the camera.
    private bool isWaitingOnStartDelay = true;

    private void Start()
    {
        StartCoroutine(EnableAfterDelay(0.1f));
    }

    private IEnumerator EnableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isWaitingOnStartDelay = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isWaitingOnStartDelay) { return; }

        if (other.gameObject.tag == "CameraTargetActor")
        {
            Camera.main.transform.position = transform.position;
        }
    }
}
