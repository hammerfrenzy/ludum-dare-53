using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltimeterStation : MonoBehaviour, IShipHazard, IInteractStation
{
    public RatSwapperBehavior RatSwapper;
    public PumpItUpMinigameBehvaior Minigame;
    public GameObject CountdownHolderForEnableDisable;
    public ShipProgressBar CountdownBar;

    public float RemainingTime => timeBeforeDeflated;
    public HazardLocation Location => HazardLocation.Altimeter;
    public bool RetainControlOnSwap => false;

    private ShipEventCoordinatorBehavior reportBackToCoordinator;
    private float timeBeforeDeflated = 0;
    private bool isDeflating = false;

    // Update is called once per frame
    void Update()
    {
        if (!isDeflating) return;

        timeBeforeDeflated -= Time.deltaTime;
        if (timeBeforeDeflated <= 0)
        {
            reportBackToCoordinator.HazardDestroyedTheShip();
        }
    }

    public void TriggerBalloonDeflation(ShipEventCoordinatorBehavior coordinator, float timeUntilDeflated)
    {
        isDeflating = true;
        reportBackToCoordinator = coordinator;
        timeBeforeDeflated = timeUntilDeflated;
        CountdownHolderForEnableDisable.SetActive(true);
        CountdownBar.SetTimeToFill(timeUntilDeflated);
    }

    public void FinishedPumping()
    {
        isDeflating = false;
        CountdownHolderForEnableDisable.SetActive(false);
        reportBackToCoordinator.HazardWasResolved(this);
    }

    public bool CanInteract()
    {
        return isDeflating;
    }

    public void SetInteracting(bool isInteracting, RatControllerBehavior rat)
    {
        if (isDeflating && isInteracting)
        {
            Minigame.StartPumpingItUp(this, rat);
            RatSwapper.SetIsInMinigame(true);
        }
    }

    // Unused IShipHazard function because duct tape
    public void AssignRat(RatControllerBehavior rat)
    {
    }

    // Unused IShipHazard function because duct tape
    public void SetCoordinatorAndLocation(ShipEventCoordinatorBehavior coordinator, HazardLocation location)
    { 
    }
}
