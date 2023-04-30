using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFireHazardBehavior : MonoBehaviour, IShipHazard, IInteractStation
{
    public bool RetainControlOnSwap { get { return true; } }

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

    public void SetInteracting(bool isInteracting, RatControllerBehavior rat)
    {
        if (isInteracting)
        {
            Debug.Log($"{rat.name} started working on the fire.");
            assignedRat = rat;
        }
        else
        {
            Debug.Log($"{rat.name} stopped working on the fire.");
            assignedRat = null;
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

    //void OnTriggerEnter(Collider other)
    //{
    //    var rat = other.gameObject.GetComponent<RatControllerBehavior>();

    //    if (rat == null) { return; }

    //    Debug.Log("Quenching the Fire");

    //    assignedRat = rat;
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    var rat = other.gameObject.GetComponent<RatControllerBehavior>();

    //    if (rat == null) { return; }

    //    var assignedRatID = assignedRat.gameObject.GetInstanceID();
    //    var thisRatID = rat.gameObject.GetInstanceID();

    //    if (assignedRatID == thisRatID)
    //    {
    //        Debug.Log("Rat Gone");
    //        assignedRat = null;
    //    }
    //}
}
