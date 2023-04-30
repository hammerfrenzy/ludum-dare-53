using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VoicelineManager : MonoBehaviour
{
    AudioManager audioManager;

    private int currentRicoQuip = 0;
    private int currentHoraceQuip = 0;
    private int currentNixieQuip = 0;

    private int currentRicoAcknowledgement = 0;
    private int currentHoraceAcknowledgement = 0;
    private int currentNixieAcknowledgement = 0;

    private string[] ricoQuips = { "RQ1", "RQ2", "RQ3", "RQ4" };
    private string[] horaceQuips = { "HQ1", "HQ2", "HQ3", "HQ4" };
    private string[] nixieQuips = { "NQ1", "NQ2", "NQ3", "NQ4" };

    private string[] ricoAcknowledgements = { "RA1", "RA2", "RA3", "RA4" };
    private string[] horaceAcknowledgements = { "HA1", "HA2", "HA3", "HA4" };
    private string[] nixieAcknowledgements = { "NA1", "NA2", "NA3", "NA4" };

    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();
    }

    public void PlayQuip(bool isRico, bool isHorace, bool isNixie)
    {
        if (isRico)
        {
            audioManager.Play(ricoQuips[currentRicoQuip]);
            
            if (currentRicoQuip < ricoQuips.Length - 1)
            {
                currentRicoQuip += 1;
            }
            else
            {
                currentRicoQuip = 0;
            }
        }

        if (isHorace)
        {
            audioManager.Play(horaceQuips[currentHoraceQuip]);

            if (currentHoraceQuip < horaceQuips.Length - 1)
            {
                currentHoraceQuip += 1;
            }
            else
            {
                currentHoraceQuip = 0;
            }
        }

        if (isNixie)
        {
            audioManager.Play(nixieQuips[currentNixieQuip]);
            
            if (currentNixieQuip < nixieQuips.Length - 1)
            {
                currentNixieQuip += 1;
            }
            else
            {
                currentNixieQuip = 0;
            }
        }
    }

    public void PlayAknowledgement(bool isRico, bool isHorace, bool isNixie)
    {
        if (isRico)
        {
            audioManager.Play(ricoAcknowledgements[currentRicoAcknowledgement]);
            
            if (currentRicoAcknowledgement < ricoAcknowledgements.Length - 1)
            {
                currentRicoAcknowledgement += 1;
            }
            else
            {
                currentRicoAcknowledgement = 0;
            }
        }

        if (isHorace)
        {
            audioManager.Play(horaceAcknowledgements[currentHoraceAcknowledgement]);
           
            if (currentHoraceAcknowledgement < horaceAcknowledgements.Length - 1)
            {
                currentHoraceAcknowledgement += 1;
            }
            else
            {
                currentHoraceAcknowledgement = 0;
            }
        }

        if (isNixie)
        {
            audioManager.Play(nixieAcknowledgements[currentNixieAcknowledgement]);

            if (currentNixieAcknowledgement < nixieAcknowledgements.Length - 1)
            {
                currentNixieAcknowledgement += 1;
            }
            else
            {
                currentNixieAcknowledgement = 0;
            }
        }
    }

    public void PlayDelivery(bool isRico, bool isHorace, bool isNixie)
    {
        if (isRico)
        {
            audioManager.Play("Rico Delivery");
        } 
        else if (isHorace)
        {
            audioManager.Play("Horace Delivery");
        }
        else if (isNixie)
        {
            audioManager.Play("Nixie Delivery");
        }
    }
}
