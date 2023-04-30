using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipBehavior : MonoBehaviour
{
    public float maxSpeed = 0.05f;

    private int currentHullIntegrity = 100;
    public int maxHullIntegrity = 100; 

    GameObject mapPosition;
    private ShipMovement shipMovement;

    HelmStation helmStation;

    ThrottleStation throttleStation;

    DropStation dropStation;
    // Start is called before the first frame update
    void Start()
    {
        mapPosition = GameObject.Find("ShipMapIcon");
        shipMovement = mapPosition.GetComponent<ShipMovement>();

        helmStation = GameObject.Find("HelmStation").GetComponent<HelmStation>();

        throttleStation = GameObject.Find("ThrottleStation").GetComponent<ThrottleStation>();

        dropStation = GameObject.Find("DropStation").GetComponent<DropStation>();
    }

    // Update is called once per frame
    void Update()
    {
        shipMovement.desiredHeading += helmStation.steeringInput * 0.0001f;
        shipMovement.speed = (throttleStation.speedPercentage/100) * maxSpeed;


        dropStation.SetDeliveryAvailable(shipMovement.isOverUninfectedInnocentTown());
        
    }

    public void DamageHullIntegrity(int damage)
    {
        currentHullIntegrity = currentHullIntegrity - damage;
    }

    public void FixHullIntegrity()
    {
        currentHullIntegrity = maxHullIntegrity;
    }
}
