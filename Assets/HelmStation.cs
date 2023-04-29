using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmStation : MonoBehaviour, InteractStation
{
    public string stationName = "Helm";

    public GameObject steeringUi;
    public float steeringInput;

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
        steeringInput += dx * 0.06f;
        steeringInput = Mathf.Clamp(steeringInput, -90f, 90f);

        Debug.Log(steeringInput);
    }

    void FixedUpdate()
    {
        steeringUi.GetComponent<SpriteRenderer>().enabled = isInteracting;

        steeringUi.transform.eulerAngles = new Vector3(0,0,-steeringInput);


    }

    public void SetInteracting(bool interacting)
    {
        isInteracting = interacting;
    }
}
