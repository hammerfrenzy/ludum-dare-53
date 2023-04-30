using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmStation : MonoBehaviour, IInteractStation
{
    public bool RetainControlOnSwap { get { return false; } }
    public string stationName = "Helm";

    public GameObject steeringUi;
    public float steeringInput;

    public float steeringSensitivity = 0.05f;

    private bool isInteracting;

    // Start is called before the first frame update
    void Start()
    {
        steeringUi = GameObject.Find("SteeringUI");
        steeringInput = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInteracting) { return; }

        var dx = Input.GetAxis("Horizontal");
        steeringInput += dx * steeringSensitivity;
        steeringInput = Mathf.Clamp(steeringInput, -90f, 90f);
    }

    void FixedUpdate()
    {
        steeringUi.GetComponent<SpriteRenderer>().enabled = isInteracting;

        steeringUi.transform.eulerAngles = new Vector3(0,0,-steeringInput);
    }

    public bool CanInteract() { return true; }

    public void SetInteracting(bool interacting, RatControllerBehavior rat)
    {
        isInteracting = interacting;
    }
}
