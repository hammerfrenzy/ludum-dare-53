using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HazardAlertUIBehavior : MonoBehaviour
{
    public Image Icon;
    public Text Text;

    public void UpdateText(HazardLocation location, int timeLeft)
    {
        Icon.enabled = true;
        Text.enabled = true;

        Text.text = $"{TextForLocation(location)} {timeLeft}"; 
    }

    public void Hide()
    {
        Icon.enabled = false;
        Text.enabled = false;
    }

    private string TextForLocation(HazardLocation location)
    {
        switch (location)
        {
            case HazardLocation.Altimeter:
                return "Altimeter";
            case HazardLocation.Bird:
                return "Bird Attack";
            case HazardLocation.BalloonRoom:
                return "Balloon Deck";
            case HazardLocation.EngineRoom:
                return "Engine Room";
            case HazardLocation.MainDeck:
                return "Main Deck";
        }

        return "";
    }
}
