using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipBehavior : MonoBehaviour
{
    private ShipMovement bearing;
    private int currentHullIntegrity = 100;
    public int maxHullIntegrity = 100; 

    GameObject mapPosition;
    private ShipMovement shipMovement;

    HelmStation helmStation;

    // Start is called before the first frame update
    void Start()
    {
        mapPosition = GameObject.Find("ShipMapIcon");
        shipMovement = mapPosition.GetComponent<ShipMovement>();

        helmStation = GameObject.Find("HelmStation").GetComponent<HelmStation>();
    }

    // Update is called once per frame
    void Update()
    {
        shipMovement.desiredHeading += helmStation.steeringInput * 0.0001f;
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
