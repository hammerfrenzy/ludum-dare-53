using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BirdSwattingMinigameBehavior : MonoBehaviour
{
    AudioManager audioManager;

    public GameObject Bird;
    public Sprite BirdSprite;
    public Sprite SadBirdSprite;
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

    private PunchGloveStation station;
    private RatControllerBehavior ratOnHarpoon;
    private SpriteRenderer spriteRenderer;
    private RatSwapperBehavior ratSwapper;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        birdStartPosition = Bird.transform.position;
        crosshairStartPosition = Crosshair.transform.position;
        spriteRenderer = Bird.GetComponent<SpriteRenderer>();
        ratSwapper = FindObjectOfType<RatSwapperBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!minigameIsActive) { return; }

        if (fireCooldown > 0) fireCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && fireCooldown <= 0)
        {
            FireHarpoon();
        }
    }

    // Start the mini-game
    public void ManTheHarpoons(PunchGloveStation station, RatControllerBehavior ratOnHarpoon)
    {
        minigameIsActive = true;
        Camera.main.transform.position = GameCameraTarget.transform.position;
        
        this.ratOnHarpoon = ratOnHarpoon;
        this.station = station;

        fireCooldown = 0;

        Bird.transform.position = birdStartPosition;
        Bird.transform.rotation = Quaternion.identity;
        var birdBody = Bird.GetComponent<Rigidbody>();
        birdBody.useGravity = false;
        birdBody.isKinematic = true;

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
        station.BirdThwarted();
        
        yield return new WaitForSeconds(delay);

        ratSwapper.SetIsInMinigame(false);
        ratOnHarpoon.ChangeControl(true);
        Camera.main.transform.position = ReturnCameraTarget.transform.position;
        spriteRenderer.sprite = BirdSprite;
    }

    public void FireHarpoon()
    {
        fireCooldown = 2f;
        var gloveObject = Instantiate(GloveTemplate, Crosshair.transform.position, Quaternion.identity);
        gloveObject.GetComponent<GloveProjectileBehavior>().SetMinigameParent(this);
        audioManager.Play("Bird Station Thump");
    }
}
