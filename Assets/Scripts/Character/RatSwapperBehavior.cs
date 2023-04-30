using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSwapperBehavior : MonoBehaviour
{
    public List<RatControllerBehavior> RatControllers;

    private CameraTargetBehavior[] cameraTargets;
    private int controlledIndex = 0;
    private bool isInMinigame = false;

    // Start is called before the first frame update
    void Start()
    {
        var firstRat = RatControllers[controlledIndex];
        firstRat.ChangeControl(true);

        cameraTargets = FindObjectsOfType<CameraTargetBehavior>();
        ActivateCameraClosestToRat(firstRat);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInMinigame) { return; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateNextRat();
        }
    }

    public void SetIsInMinigame(bool isInMinigame)
    {
        this.isInMinigame = isInMinigame;
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
        Camera.main.orthographicSize = 5.0f;
        Camera.main.transform.position = closestTarget.transform.position;
    }
}
