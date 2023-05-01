using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionStationBehavior : MonoBehaviour, IInteractStation
{
    public CameraTargetBehavior InstructionCameraTarget;
    public CameraTargetBehavior ReturnCameraTarget;    

    public bool RetainControlOnSwap => false;

    public bool CanInteract()
    {
        return true;
    }

    public void SetInteracting(bool interacting, RatControllerBehavior rat)
    {
        if (interacting)
        {
            Camera.main.transform.position = InstructionCameraTarget.transform.position;
        }
        else
        {
            Camera.main.transform.position = ReturnCameraTarget.transform.position;
        }
    }
}
