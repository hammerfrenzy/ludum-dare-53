using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpItUpMinigameBehvaior : MonoBehaviour
{
    public CameraTargetBehavior MinigameCameraTarget;
    public CameraTargetBehavior ReturnCameraTarget;
    public ShipProgressBar ProgressBar;

    public Sprite PumpUp;
    public Sprite PumpDown;
    public SpriteRenderer PumpRenderer;

    private RatControllerBehavior returnToRat;
    private AltimeterStation station;
    private bool isPlayingMinigame = false;
    private bool isPumpUp = false;
    private int requiredPumps = 10;
    private int numberOfPumps = 0;

    // Start is called before the first frame update
    void Start()
    {
        // we'll manually increment this one
        ProgressBar.SetMakeProgress(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayingMinigame) { return; }

        if (isPumpUp && Input.GetKeyDown(KeyCode.S))
        {
            TogglePump();

        }
        else if (!isPumpUp && Input.GetKeyDown(KeyCode.W))
        {
            TogglePump();
        }
    }

    private void TogglePump()
    {
        isPumpUp = !isPumpUp;
        
        if (!isPumpUp)
        {
            numberOfPumps++;
            ProgressBar.ManuallyProgress(1);
        }

        var newSprite = isPumpUp ? PumpUp : PumpDown;
        PumpRenderer.sprite = newSprite;

        if (numberOfPumps >= requiredPumps)
        {
            WeArePumpedToTheMax();
        }
    }

    public void StartPumpingItUp(AltimeterStation station, RatControllerBehavior rat)
    {
        this.station = station;
        returnToRat = rat;
        isPlayingMinigame = true;
        numberOfPumps = 0;
        isPumpUp = false;
        PumpRenderer.sprite = PumpDown;
        ProgressBar.SetTimeToFill(requiredPumps);

        Camera.main.transform.position = MinigameCameraTarget.transform.position;
    }

    private void WeArePumpedToTheMax()
    {
        StartCoroutine(EndAfterDelay(0.5f));
    }

    public IEnumerator EndAfterDelay(float delay)
    {
        station.FinishedPumping();

        yield return new WaitForSeconds(delay);

        returnToRat.ChangeControl(true);
        station.FinishedPumping();
        Camera.main.transform.position = ReturnCameraTarget.transform.position;
    }
}
