using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehavior : MonoBehaviour
{
    private ShipMovement bearing;
    private int hullIntegrity = 100;


    // Start is called before the first frame update
    void Start()
    {
        bearing = GetComponent<ShipMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
