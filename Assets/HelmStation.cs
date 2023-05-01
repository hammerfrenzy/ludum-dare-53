using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmStation : MonoBehaviour, IInteractStation
{
    public bool RetainControlOnSwap { get { return false; } }
    public string stationName = "Helm";

    public GameObject HeadingIndicator;
    public Transform FullLeftTransform;
    public Transform FullRightTransform;

    MeshRenderer helmTutorial;

    /// <summary>
    /// Used by ShipBehavior to update ship movement.
    /// Is a range from -90 to 90.
    /// </summary>
    public float SteeringInput
    {
        get
        {
            return (heading * 180) - 90;
        }
    }

    //public float steeringInput;
    //public GameObject steeringUi;

    private float steeringSensitivity = 0.20f;
    private float heading = 0.5f;

    private bool isInteracting;

    // Start is called before the first frame update
    void Start()
    {
        //steeringUi = GameObject.Find("SteeringUI");
        //steeringInput = 0.0f;

        UpdateHeadingIndicator();
        helmTutorial = GameObject.Find("HelmTutorial").GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        helmTutorial.enabled = isInteracting;
        if (!isInteracting) { return; }

        var dx = Input.GetAxis("Horizontal");

        heading += dx * steeringSensitivity * Time.deltaTime;
        heading = Mathf.Clamp01(heading);
        UpdateHeadingIndicator();        
    }

    private void UpdateHeadingIndicator()
    {
        HeadingIndicator.transform.position = Vector3.Lerp(FullLeftTransform.position, FullRightTransform.position, heading);
    }

    //void FixedUpdate()
    //{
    //    steeringUi.GetComponent<SpriteRenderer>().enabled = isInteracting;

    //    steeringUi.transform.eulerAngles = new Vector3(0,0,-steeringInput);
    //}

    public bool CanInteract() { return true; }

    public void SetInteracting(bool interacting, RatControllerBehavior rat)
    {
        isInteracting = interacting;
    }
}
