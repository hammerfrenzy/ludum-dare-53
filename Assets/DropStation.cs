using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Random = UnityEngine.Random;

public class DropStation : MonoBehaviour, IInteractStation
{
    private bool isAvailable;
    private bool isInteracting;
    SpriteRenderer interactUi;
    SpriteRenderer leverUi;
    private RatControllerBehavior interactingRat;
    VoicelineManager voiceManager;
    AudioManager audioManager;
    private float leverProgress = 0f;

    private bool isStuck = true;
    private float stuckLength = 100f;
    private float stuckProgress = 0f;
    public bool RetainControlOnSwap { get { return false; } }

    private InnocentTown innocentTown = null;
    CargoSpawnerBehavior cargoSpawnerBehavior;

    MeshRenderer dropTutorial;

    // Start is called before the first frame update
    void Start()
    {
        interactUi = GameObject.Find("ExclamationMarkUI").GetComponent<SpriteRenderer>();
        leverUi = GameObject.Find("DeliveryLeverUI").GetComponent<SpriteRenderer>();
        cargoSpawnerBehavior = GameObject.FindObjectOfType<CargoSpawnerBehavior>();
        voiceManager = FindObjectOfType<VoicelineManager>();
        audioManager = FindObjectOfType<AudioManager>();
        dropTutorial = GameObject.Find("DropTutorial").GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        interactUi.GetComponent<SpriteRenderer>().enabled = isAvailable;
        dropTutorial.enabled = isInteracting;

        if (!isInteracting) { return; }

        interactUi.GetComponent<SpriteRenderer>().enabled = false;
        
        var dx = Input.GetAxis("Horizontal");
        
        if (dx > 0)
        {
            if (isStuck)
            {
                if(stuckProgress > 0.5f && stuckProgress < 1.5f)
                {
                    audioManager.Play("Lever Stuck");
                }
                leverUi.transform.eulerAngles = new Vector3(0, 0, Random.Range(10, 30));
                stuckProgress += dx * 0.7f + Time.deltaTime;
                if(stuckProgress > stuckLength)
                {
                    isStuck = false;
                }
            }
            else
            {
                leverProgress += dx * 68.0f * Time.deltaTime;
                leverProgress = Mathf.Clamp(leverProgress, 0f, 100f);
                leverUi.transform.eulerAngles = new Vector3(0, 0, -110 * (leverProgress / 100) + 10);
                if(leverProgress == 100)
                {
                    audioManager.Play("Delivery Chute Open");
                    cargoSpawnerBehavior.MakeDelivery();
                    ResetProgress();
                    voiceManager.PlayDelivery(interactingRat.isRico, interactingRat.isHorace, interactingRat.isNixie);
                    interactingRat.ChangeControl(true, false);
                    interactingRat = null;
                    if (innocentTown != null) innocentTown.InfectTown();
                }
            }
        }
        else
        {
            ResetProgress();
        }
    }

    public void SetDeliveryAvailable(bool available)
    {
        isAvailable = available;
    }

    public void SetInteracting(bool interacting, RatControllerBehavior rat)
    {
        isInteracting = interacting;
        interactingRat = rat;
    }

    public bool CanInteract()
    {
        return isAvailable;
    }

    public void SetInnocentTown(InnocentTown town)
    {
        innocentTown = town;
    }

    private void ResetProgress()
    {
        isStuck = true;
        stuckProgress = 0;
        leverProgress = 0;
        leverUi.transform.eulerAngles = new Vector3(0, 0, 10);
    }
}
