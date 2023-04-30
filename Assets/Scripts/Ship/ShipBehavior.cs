using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    InnocentTown[] innocentTowns;

    // Start is called before the first frame update
    void Start()
    {
        mapPosition = GameObject.Find("ShipMapIcon");
        shipMovement = mapPosition.GetComponent<ShipMovement>();

        helmStation = GameObject.Find("HelmStation").GetComponent<HelmStation>();

        throttleStation = GameObject.Find("ThrottleStation").GetComponent<ThrottleStation>();

        dropStation = GameObject.Find("DropStation").GetComponent<DropStation>();

        innocentTowns = GameObject.FindObjectsOfType<InnocentTown>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!innocentTowns.Any(town => !town.infected))
        {
            SceneManager.LoadScene("GameOver");
        }

        shipMovement.desiredHeading += helmStation.steeringInput * 0.0001f;
        shipMovement.speed = (throttleStation.speedPercentage/100) * maxSpeed;


        dropStation.SetDeliveryAvailable(shipMovement.isOverUninfectedInnocentTown());
        dropStation.SetInnocentTown(shipMovement.currentTown);
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
