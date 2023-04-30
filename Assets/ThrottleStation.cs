using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleStation : MonoBehaviour, IInteractStation
{
    public bool RetainControlOnSwap { get { return false; } }

    private bool isInteracting;

    public GameObject throttleLeverUI;
    public float speedPercentage;

    // Start is called before the first frame update
    void Start()
    {
        throttleLeverUI = GameObject.Find("ThrottleLeverUI");
    }

    void Update()
    {
        if (!isInteracting) { return; }

        var dx = Input.GetAxis("Horizontal");
        speedPercentage += -dx * 22.0f * Time.deltaTime;
        speedPercentage = Mathf.Clamp(speedPercentage, 0f, 100f);
    }
    void FixedUpdate()
    {
        //-10 to -270 z rotation
        throttleLeverUI.transform.eulerAngles = new Vector3(0, 0, 260*(speedPercentage/100));
    }

    public bool CanInteract() { return true; }

    // Update is called once per frame
    public void SetInteracting(bool interacting, RatControllerBehavior rat)
    {
        isInteracting = interacting;
    }
}
