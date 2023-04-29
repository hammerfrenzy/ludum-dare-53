using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipBehavior : MonoBehaviour
{
    public float maxSpeed = 0.05f;

    private ShipMovement bearing;
    private int currentHullIntegrity = 100;
    public int maxHullIntegrity = 100; 

    GameObject mapPosition;
    private ShipMovement shipMovement;

    HelmStation helmStation;

    ThrottleStation throttleStation;
    // Start is called before the first frame update
    void Start()
    {
        mapPosition = GameObject.Find("ShipMapIcon");
        shipMovement = mapPosition.GetComponent<ShipMovement>();

        helmStation = GameObject.Find("HelmStation").GetComponent<HelmStation>();

        throttleStation = GameObject.Find("ThrottleStation").GetComponent<ThrottleStation>();
    }

    // Update is called once per frame
    void Update()
    {
        shipMovement.desiredHeading += helmStation.steeringInput * 0.0001f;
        shipMovement.speed = (throttleStation.speedPercentage/100) * maxSpeed;
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
