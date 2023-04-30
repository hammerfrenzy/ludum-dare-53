using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchGloveStation : MonoBehaviour, IInteractStation
{
    public BirdSwattingMinigameBehavior Minigame;
    public bool RetainControlOnSwap { get { return false; } }

    public void SetInteracting(bool isInteracting, RatControllerBehavior rat)
    {
        if (isInteracting) // TODO: && Bird is attacking
        {
            Minigame.ManTheHarpoons(rat);
        }
    }
}
