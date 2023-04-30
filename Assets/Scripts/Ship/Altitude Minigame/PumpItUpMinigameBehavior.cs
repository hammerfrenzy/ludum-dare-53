using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpItUpMinigameBehvaior : MonoBehaviour
{
    public CameraTargetBehavior MinigameCameraTarget;
    public CameraTargetBehavior ReturnCameraTarget;

    public Sprite PumpUp;
    public Sprite PumpDown;
    public SpriteRenderer PumpRenderer;

    private RatControllerBehavior returnToRat;
    private AltimeterStation station;
    private bool isPlayingMinigame = false;
    private bool isPumpUp = false;
    private int numberOfPumps = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayingMinigame) { return; }

        if (isPumpUp && Input.GetKeyDown(KeyCode.S))
        {
            Pump();

        }
        else if (!isPumpUp && Input.GetKeyDown(KeyCode.W))
        {
            Pump();
        }
    }

    private void Pump()
    {
        numberOfPumps++;
        isPumpUp = !isPumpUp;

        var newSprite = isPumpUp ? PumpUp : PumpDown;
        PumpRenderer.sprite = newSprite;

        if (numberOfPumps > 10)
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
