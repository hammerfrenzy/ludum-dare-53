using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatControllerBehavior : MonoBehaviour
{

    Animator animator;
    VoicelineManager voiceManager;

    public float Speed = 5.0f;
    public GameObject selectedTriangle;
    private CharacterController characterController;
    private SpriteRenderer spriteRenderer;

    private bool isOnLadder = false;
    private bool isBeingControlled = false;

    [Header("Rat Identification")]
    public bool isRico;
    public bool isHorace;
    public bool isNixie;

    [Header("Animation State Flags")]
    public bool isWalking = false;
    public bool isClimbing = false;
    public bool isInteracting = false;

    private IInteractStation currentInteractStation = null;

    // Start is called before the first frame update
    private void Awake()
    {
        voiceManager = FindObjectOfType<VoicelineManager>();
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");


        if (!isBeingControlled)
        {
            dx = 0;
            isWalking = false;
            isClimbing = false;
            isInteracting = false;

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
                animator.SetBool("isInteracting", isInteracting);
                currentInteractStation.SetInteracting(isInteracting, this);

                if (isInteracting)
                {
                    voiceManager.PlayAknowledgement(isRico, isHorace, isNixie);
                }
            }

            if (isInteracting)
            {
                dx = 0;
            }

            if (isOnLadder)
            {
                isClimbing = true;
                animator.SetBool("isClimbing", true);
            }

            if (dx == 0)
            {
                isWalking = false;
                animator.SetBool("isWalking", false);
                FindObjectOfType<AudioManager>().Play("Walking (Metal Floor)");
            }
            else
            {
                isWalking = true;
                animator.SetBool("isWalking", true);

                if(dx < 0)
                {
                    spriteRenderer.flipX = true;
                } 
                else
                {
                    spriteRenderer.flipX = false;
                }
            }
        }

        // fake gravity
        if (!isOnLadder) 
        { 
            dy = -4;
            isClimbing = false;
            animator.SetBool("isClimbing", false);
        }
        
        var movement = new Vector2(dx, dy);
        characterController.Move(movement * Speed * Time.deltaTime);
    }

    public void ChangeControl(bool giveControl)
    {
        if (currentInteractStation != null)
        {
            // Don't stop important things like putting 
            // out a fire just because we changed rats.
            var stationKeepsWorking = currentInteractStation.RetainControlOnSwap;
            if (stationKeepsWorking)
            {
                // Was previously set to false during update while not in control.
                // Shouldn't need to call SetInteracting again though.
                isInteracting = true;
            }
            else
            {
                isInteracting = false;
                currentInteractStation.SetInteracting(isInteracting, this);
            }
        }       

        isBeingControlled = giveControl;
        selectedTriangle.GetComponent<SpriteRenderer>().enabled = giveControl;

        if (giveControl && voiceManager != null) 
        {
            voiceManager.PlayQuip(isRico, isHorace, isNixie);
        }
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
            currentInteractStation = other.GetComponent<IInteractStation>();
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
