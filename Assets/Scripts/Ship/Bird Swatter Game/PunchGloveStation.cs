using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchGloveStation : MonoBehaviour, InteractStation
{
    public BirdSwattingMinigameBehavior Minigame;

    public void SetInteracting(bool isInteracting, RatControllerBehavior rat)
    {
        if (isInteracting) // TODO: && Bird is attacking
        {
            Minigame.ManTheHarpoons(rat);
        }
    }
}
