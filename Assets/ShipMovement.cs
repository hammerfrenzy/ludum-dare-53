using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float desiredHeading;
    public float speed;
    public Vector3 currentVector;
    public float turnSpeed;
    public float turnProgress;

    public InnocentTown currentTown = null;

    public bool isAtBorder;

    // Start is called before the first frame update
    void Start()
    {
        desiredHeading = 0;
        var xComponent = Mathf.Sin(Mathf.Deg2Rad * desiredHeading);
        var yComponent = Mathf.Cos(Mathf.Deg2Rad * desiredHeading);
        var desiredVector = new Vector3((float)(xComponent * Time.deltaTime), (float)(yComponent * Time.deltaTime)) * speed;
        currentVector = desiredVector;

        speed = 0.05f;
        turnProgress = 0f;
        turnSpeed = 0.005f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAtBorder)
        {
            desiredHeading += 180;
            isAtBorder = false;
        }
        var xComponent = Mathf.Sin(Mathf.Deg2Rad * desiredHeading);
        var yComponent = Mathf.Cos(Mathf.Deg2Rad * desiredHeading);
        var desiredVector = new Vector3(xComponent, yComponent);

        if (desiredVector != currentVector)
        {
            turnProgress += turnSpeed * Time.deltaTime;
        }

        if(desiredVector == currentVector)
        {
            turnProgress = 0.0f;
        }
        
        currentVector = Vector3.Lerp(currentVector, desiredVector, turnProgress);

        transform.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(new Vector3(0, 1, 0), currentVector, new Vector3(0, 0, 1)));
        transform.position += speed * Time.deltaTime * currentVector;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MapBorder")
        {
            isAtBorder = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "InnocentTown")
        {
            currentTown = other.GetComponent<InnocentTown>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "InnocentTown")
        {
            currentTown = null;
        }

        if (other.gameObject.tag == "MapBorder")
        {
            isAtBorder = false;
        }
    }

    public bool isOverUninfectedInnocentTown()
    {
        if(currentTown != null && !currentTown.infected)
        {
            return true;
        }
        return false;
    }
}
