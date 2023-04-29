using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatControllerBehavior : MonoBehaviour
{
    public float Speed = 5.0f;
    public GameObject selectedTriangle;
    private CharacterController characterController;

    private bool isOnLadder = false;
    private bool isBeingControlled = false;
    private bool isInteracting = false;

    private InteractStation currentInteractStation = null;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");


        if(!isBeingControlled)
        {
            dx = 0;

            if(isOnLadder)
            {
                dy = 0;
            }
        }

        if (isBeingControlled)
        {
            if (Input.GetKeyDown(KeyCode.E) && currentInteractStation != null)
            {
                isInteracting = !isInteracting;
                currentInteractStation.SetInteracting(isInteracting);
            }

            if(isInteracting)
            {
                dx = 0;
            }

            if (dx == 0)
            {
                FindObjectOfType<AudioManager>().Play("Walking (Metal Floor)");
            }
        }

        // fake gravity
        if (!isOnLadder) 
        { 
            dy = -4;
        }
        
        var movement = new Vector2(dx, dy);
        characterController.Move(movement * Speed * Time.deltaTime);

    }

    public void ChangeControl(bool giveControl)
    {
        if(!giveControl && currentInteractStation != null)
        {
            isInteracting = false;
            currentInteractStation.SetInteracting(isInteracting);
        }
        isBeingControlled = giveControl;
        selectedTriangle.GetComponent<SpriteRenderer>().enabled = giveControl;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            isOnLadder = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Station")
        {
            currentInteractStation = other.GetComponent<InteractStation>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            isOnLadder = false;
        }

        if (other.gameObject.tag == "Station")
        {
            currentInteractStation = null;
        }
    }
}
