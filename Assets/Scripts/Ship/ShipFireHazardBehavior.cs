using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFireHazardBehavior : MonoBehaviour, IShipHazard, IInteractStation
{
    public ShipProgressBar DoomBar;
    public ShipProgressBar ProgressBar;

    public HazardLocation Location { get { return location; } }
    public float RemainingTime { get { return timeBeforeRuin; } }
    public bool RetainControlOnSwap { get { return true; } }

    private ShipEventCoordinatorBehavior coordinator = null;
    private RatControllerBehavior assignedRat = null;
    private HazardLocation location;
    private float timeToDouse = 15;
    private float timeBeforeRuin = 25;

    void Start()
    {
        DoomBar.SetTimeToFill(timeBeforeRuin);
        ProgressBar.SetTimeToFill(timeToDouse);
        ProgressBar.SetMakeProgress(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (assignedRat != null)
        {
            timeToDouse -= Time.deltaTime;

            if (timeToDouse <= 0)
            {
                Resolve();
                return;
            }
        }
        else
        {
            timeBeforeRuin -= Time.deltaTime;

            if (timeBeforeRuin <= 0)
            {
                coordinator.HazardDestroyedTheShip();
            }
        }       
    }

    public bool CanInteract() { return true; }

    public void SetInteracting(bool isInteracting, RatControllerBehavior rat)
    {
        if (isInteracting)
        {
            Debug.Log($"{rat.name} started working on the fire.");
            assignedRat = rat;
            ProgressBar.SetMakeProgress(true);
            DoomBar.SetMakeProgress(false);
        }
        else
        {
            Debug.Log($"{rat.name} stopped working on the fire.");
            assignedRat = null;
            ProgressBar.SetMakeProgress(false);
            DoomBar.SetMakeProgress(true);
        }
    }

    public void SetCoordinatorAndLocation(ShipEventCoordinatorBehavior coordinator, HazardLocation location)
    {
        this.coordinator = coordinator;
        this.location = location;
    }

    public void AssignRat(RatControllerBehavior assignedRat)
    {
        this.assignedRat = assignedRat;
    }

    public void Resolve()
    {
        if (assignedRat != null)
        {
            assignedRat.HazardHasCompleted();    
        }

        coordinator.HazardWasResolved(this);
        Destroy(gameObject);
    }
}
