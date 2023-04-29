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

    // Start is called before the first frame update
    void Start()
    {
        desiredHeading = 0;
        var xComponent = Mathf.Sin(Mathf.Deg2Rad * desiredHeading);
        var yComponent = Mathf.Cos(Mathf.Deg2Rad * desiredHeading);
        var desiredVector = new Vector3((float)(xComponent * Time.deltaTime), (float)(yComponent * Time.deltaTime)) * speed;
        currentVector = desiredVector;

        speed = 0.020f;
        turnProgress = 0f;
        turnSpeed = 0.0000005f;
    }

    // Update is called once per frame
    void Update()
    {
        var xComponent = Mathf.Sin(Mathf.Deg2Rad * desiredHeading);
        var yComponent = Mathf.Cos(Mathf.Deg2Rad * desiredHeading);
        var desiredVector = new Vector3(xComponent, yComponent);

        if (desiredVector != currentVector)
        {
            turnProgress += turnSpeed;
        }

        if(desiredVector == currentVector)
        {
            turnProgress = 0.0f;
        }

        currentVector = Vector3.Lerp(currentVector, desiredVector, turnProgress);

        transform.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(new Vector3(0, 1, 0), currentVector, new Vector3(0, 0, 1)));
        transform.position += speed * Time.deltaTime * currentVector;
    }
}
