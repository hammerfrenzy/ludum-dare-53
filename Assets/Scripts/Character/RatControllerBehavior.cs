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

        // fake gravity
        if (!isOnLadder) { dy = -4; }
        
        var movement = new Vector2(dx, dy);
        characterController.Move(movement * Speed * Time.deltaTime);
    }

    public void ChangeControl(bool giveControl)
    {
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            isOnLadder = false;
        }
    }
}
