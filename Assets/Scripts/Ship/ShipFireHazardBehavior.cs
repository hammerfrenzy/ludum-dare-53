using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFireHazardBehavior : MonoBehaviour, IShipHazard
{
    private ShipEventCoordinatorBehavior coordinator = null;
    private RatControllerBehavior assignedRat = null;
    private HazardLocation location;
    private float timeToDouse = 10;
    private float timeBeforeRuin = 25;

    // Start is called before the first frame update
    void Start()
    {
        
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

        timeBeforeRuin -= Time.deltaTime;

        if (timeBeforeRuin <= 0)
        {
            coordinator.HazardDestroyedTheShip();
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
        coordinator.HazardWasResolved(location);
        Destroy(gameObject);
    }
}
