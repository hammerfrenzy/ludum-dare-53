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

    public GameObject steeringUi;
    public SpriteRenderer indicatorRenderer;

    private float steeringSensitivity = 0.20f;
    private float heading = 0.5f;

    private bool isInteracting;

    // Start is called before the first frame update
    void Start()
    {
        steeringUi = GameObject.Find("SteeringUI");

        //UpdateHeadingIndicator();
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
        UpdateHeadingIndicator(dx);        
    }

    private void UpdateHeadingIndicator(float dx)
    {
        HeadingIndicator.transform.position = Vector3.Lerp(FullLeftTransform.position, FullRightTransform.position, heading);
        
        if (dx == 0)
        {
            indicatorRenderer.flipX = indicatorRenderer.flipX;
        }
        else if (dx < 0)
        {
            indicatorRenderer.flipX = true;
        }
        else if (dx > 0)
        {
            indicatorRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        var angle = 180 * heading - 90;
        steeringUi.transform.eulerAngles = new Vector3(0, 0, -angle);
    }

    public bool CanInteract() { return true; }

    public void SetInteracting(bool interacting, RatControllerBehavior rat)
    {
        isInteracting = interacting;
    }
}
