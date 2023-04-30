using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchGloveStation : MonoBehaviour, IInteractStation, IShipHazard
{
    public RatSwapperBehavior RatSwapper;
    public BirdSwattingMinigameBehavior Minigame;
    public GameObject CountdownHolderForEnableDisable;
    public ShipProgressBar CountdownBar;

    public HazardLocation Location { get { return HazardLocation.Bird; } }
    public float RemainingTime { get { return timeUntilBirdDestruction; } }
    public bool RetainControlOnSwap { get { return false; } }

    private ShipEventCoordinatorBehavior reportBackToCoordinator;
    private bool isBirdAttacking = false;

    private float timeUntilBirdDestruction = 0;

    void Update()
    {
        if (!isBirdAttacking) return;

        timeUntilBirdDestruction -= Time.deltaTime;
        if (timeUntilBirdDestruction <= 0)
        {
            reportBackToCoordinator.HazardDestroyedTheShip();
        }
    }

    public void TriggerBirdAttack(ShipEventCoordinatorBehavior coordinator, float timeUntilBirdDestroysEverything)
    {
        isBirdAttacking = true;
        reportBackToCoordinator = coordinator;
        CountdownHolderForEnableDisable.SetActive(true);
        timeUntilBirdDestruction = timeUntilBirdDestroysEverything;
        CountdownBar.SetTimeToFill(timeUntilBirdDestroysEverything);
    }

    public void BirdThwarted()
    {
        isBirdAttacking = false;
        CountdownHolderForEnableDisable.SetActive(false);
        reportBackToCoordinator.HazardWasResolved(this);
    }

    public bool CanInteract() 
    {
        return isBirdAttacking; 
    }

    public void SetInteracting(bool isInteracting, RatControllerBehavior rat)
    {
        if (isInteracting && isBirdAttacking)
        {
            Minigame.ManTheHarpoons(this, rat);
            RatSwapper.SetIsInMinigame(true);
        }
    }

    // Unused IShipHazard function because duct tape
    public void SetCoordinatorAndLocation(ShipEventCoordinatorBehavior coordinator, HazardLocation location)
    {
    }

    // Unused IShipHazard function because duct tape
    public void AssignRat(RatControllerBehavior assignedRat)
    {
    }
}
