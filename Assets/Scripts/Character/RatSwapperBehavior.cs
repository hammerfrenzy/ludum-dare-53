using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RatSwapperBehavior : MonoBehaviour
{
    public List<RatControllerBehavior> RatControllers;

    private CameraTargetBehavior[] cameraTargets;
    private int controlledIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        RatControllers[controlledIndex].ChangeControl(true);
        cameraTargets = FindObjectsOfType<CameraTargetBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateNextRat();
        }
    }

    private void ActivateNextRat()
    {
        controlledIndex++;
        if (controlledIndex >= RatControllers.Count)
        {
            controlledIndex = 0;
        }

        for (int i = 0; i < RatControllers.Count; i++)
        {
            var ratController = RatControllers[i];
            var giveControl = controlledIndex == i;
            ratController.ChangeControl(giveControl);

            // Activate the closest camera target to this rat
            if (giveControl)
            {
                ActivateCameraClosestToRat(ratController);
            }
        }
    }

    private void ActivateCameraClosestToRat(RatControllerBehavior rat)
    {
        CameraTargetBehavior closestTarget = null;
        var closestDistance = float.MaxValue;
        var ratPosition = rat.transform.position;
        for (int i = 0; i < cameraTargets.Length; i++)
        {
            var cameraTarget = cameraTargets[i];
            var distance = Vector3.Distance(ratPosition, cameraTarget.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = cameraTarget;
            }
        }

        Camera.main.transform.position = closestTarget.transform.position;
    }
}
