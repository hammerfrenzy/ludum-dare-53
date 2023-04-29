using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BirdSwattingMinigameBehavior : MonoBehaviour
{
    public GameObject Bird;
    public GameObject Crosshair;
    public GameObject GloveTemplate;
    public CameraTargetBehavior GameCameraTarget;
    public CameraTargetBehavior ReturnCameraTarget;

    private Tween birdMovementTween;
    private Tween crosshairMovementTween;

    private Vector3 birdStartPosition;
    private Vector3 crosshairStartPosition;

    private bool minigameIsActive = false;
    private float fireCooldown = 0f;

    private InteractStation stationToDisable;
    private RatControllerBehavior ratOnHarpoon;

    // Start is called before the first frame update
    void Start()
    {
        birdStartPosition = Bird.transform.position;
        crosshairStartPosition = Crosshair.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!minigameIsActive) { return; }

        if (fireCooldown > 0) fireCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A) && fireCooldown <= 0)
        {
            FireHarpoon();
        }
    }

    // Start the mini-game
    public void ManTheHarpoons(PunchGloveStation stationToDisableOnComplete, RatControllerBehavior ratOnHarpoon)
    {
        minigameIsActive = true;
        stationToDisable = stationToDisableOnComplete;
        this.ratOnHarpoon = ratOnHarpoon;
        Camera.main.transform.position = GameCameraTarget.transform.position;

        fireCooldown = 0;

        Bird.transform.position = birdStartPosition;
        Crosshair.transform.position = crosshairStartPosition;

        // Random speeds so you can't memorize the pattern
        var birdSpeed = Random.Range(2, 3);
        birdMovementTween = Bird
            .transform
            .DOMoveY(-5f, birdSpeed)
            .SetRelative(true)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);

        var crosshairSpeed = Random.Range(1.5f, 2);
        crosshairMovementTween = Crosshair
            .transform
            .DOMoveY(5f, crosshairSpeed)
            .SetRelative(true)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void EndTheHarpoons()
    {
        minigameIsActive = false;
        birdMovementTween.Kill();
        crosshairMovementTween.Kill();

        var birdBody = Bird.GetComponent<Rigidbody>();
        var torque = Random.Range(-4, 4);
        birdBody.AddTorque(new Vector3(0, 0, torque), ForceMode.Impulse);
        birdBody.useGravity = true;
        birdBody.isKinematic = false;

        StartCoroutine(EndAfterDelay(1.5f));
    }

    public IEnumerator EndAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        stationToDisable.SetInteracting(false, ratOnHarpoon);
        Camera.main.transform.position = ReturnCameraTarget.transform.position;
    }

    public void FireHarpoon()
    {
        fireCooldown = 2f;
        var gloveObject = Instantiate(GloveTemplate, Crosshair.transform.position, Quaternion.identity);
        gloveObject.GetComponent<GloveProjectileBehavior>().SetMinigameParent(this);
    }
}
