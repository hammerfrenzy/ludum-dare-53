using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchGloveStation : MonoBehaviour, IInteractStation
{
    public BirdSwattingMinigameBehavior Minigame;
    public GameObject CountdownHolderForEnableDisable;
    public ShipProgressBar CountdownBar;

    public bool RetainControlOnSwap { get { return false; } }

    private ShipEventCoordinatorBehavior reportBackToCoordinator;
    private bool IsBirdAttacking = false;

    private float timeUntilBirdDestruction = 0;

    void Update()
    {
        if (!IsBirdAttacking) return;

        timeUntilBirdDestruction -= Time.deltaTime;
        if (timeUntilBirdDestruction <= 0)
        {
            reportBackToCoordinator.HazardDestroyedTheShip();
        }
    }

    public void TriggerBirdAttack(ShipEventCoordinatorBehavior coordinator, float timeUntilBirdDestroysEverything)
    {
        IsBirdAttacking = true;
        reportBackToCoordinator = coordinator;
        CountdownHolderForEnableDisable.SetActive(true);
        timeUntilBirdDestruction = timeUntilBirdDestroysEverything;
        CountdownBar.SetTimeToFill(timeUntilBirdDestroysEverything);
    }

    public void BirdThwarted()
    {
        IsBirdAttacking = false;
        CountdownHolderForEnableDisable.SetActive(false);
        reportBackToCoordinator.HazardWasResolved(HazardLocation.Bird);
    }

    public bool CanInteract() 
    {
        return IsBirdAttacking; 
    }

    public void SetInteracting(bool isInteracting, RatControllerBehavior rat)
    {
        if (isInteracting && IsBirdAttacking)
        {
            Minigame.ManTheHarpoons(this, rat);
        }
    }
}
