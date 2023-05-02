using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IShipHazard
{
    float RemainingTime { get; }
    HazardLocation Location { get; }
    void SetCoordinatorAndLocation(ShipEventCoordinatorBehavior coordinator, HazardLocation location);
    void AssignRat(RatControllerBehavior rat);
}

public enum HazardLocation
{
    MainDeck, EngineRoom, BalloonRoom, Altimeter, Bird
}

public class ShipEventCoordinatorBehavior : MonoBehaviour
{
    public GameObject FireHazardTemplate;
    public ShipBehavior ShipBehavior;
    public PunchGloveStation PunchGloveStation;
    public AltimeterStation AltimeterStation;
    public RatSwapperBehavior RatSwapper;

    public Transform MainDeckHazardTransform;
    public Transform EngineRoomHazardTransform;
    public Transform BalloonDeckHazardTransform;

    public List<HazardAlertUIBehavior> alertUIBehaviors;

    private AudioManager audioManager;

    // Would be cool to show "completion time"
    // time2speedrun
    private float sceneStartTime;

    private float timeToNextHazard = float.MaxValue;

    private HashSet<HazardLocation> availableHazardLocations;
    private List<IShipHazard> activeHazards;

    private float defaultMinHazardTime = 20;
    private float defaultMaxHazardTime = 30;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        sceneStartTime = Time.time;

        activeHazards = new List<IShipHazard>();

        availableHazardLocations = new HashSet<HazardLocation>
        {
            HazardLocation.Altimeter,
            HazardLocation.Bird,
            HazardLocation.BalloonRoom,
            HazardLocation.EngineRoom,
            HazardLocation.MainDeck,
        };

        foreach (var alert in alertUIBehaviors)
        {
            alert.Hide();
        }

        // First hazard should be 5-7 seconds after first delivery
        timeToNextHazard = Random.Range(5, 7);
    }

    // Update is called once per frame
    void Update()
    {
        // No hazards until first delivery
        if (!ShipBehavior.HasStartedInfecting) { return; }

        timeToNextHazard -= Time.deltaTime;
        if (timeToNextHazard <= 0)
        {
            SpawnHazard();
        }

        for (int i = 0; i < alertUIBehaviors.Count; i++)
        {
            var alertBehavior = alertUIBehaviors[i];

            if (i < activeHazards.Count)
            {
                var hazard = activeHazards[i];
                alertBehavior.UpdateText(hazard.Location, (int)hazard.RemainingTime);
            } 
            else
            {
                alertBehavior.Hide();
            }
        }
    }

    void SpawnHazard()
    {
        if (availableHazardLocations.Count == 0)
        {
            // They didn't time out but you didn't fix them?
            // Or do we just give the player some grace?
            HazardDestroyedTheShip();
            return;
        }

        var index = Random.Range(0, availableHazardLocations.Count);
        var location = availableHazardLocations.ElementAt(index);// [index];
        availableHazardLocations.Remove(location);

        // Test specific hazards by uncommenting below
        //location = HazardLocation.Bird;

        switch (location)
        {
            case HazardLocation.Altimeter:
                StartPumpMinigame();
                break;
            case HazardLocation.Bird:
                StartBirdMinigame();
                break;
            case HazardLocation.BalloonRoom:
            case HazardLocation.EngineRoom:
            case HazardLocation.MainDeck:
                StartAFire(location);
                break;
        }
        
        // TODO: Difficulty scaling over time?
        timeToNextHazard = Random.Range(defaultMinHazardTime, defaultMaxHazardTime);
    }

    public void StartAFire(HazardLocation location)
    {
        audioManager.Play("Explosion");
        audioManager.Play("Fire Loop");
        Vector3 firePosition;
        switch (location)
        {
            case HazardLocation.MainDeck:
                firePosition = MainDeckHazardTransform.position;
                GameObject.Find("Main Deck Fire").GetComponent<SpriteRenderer>().enabled = true;
                break;
            case HazardLocation.EngineRoom:
                firePosition = EngineRoomHazardTransform.position;
                GameObject.Find("Engine Room Fire").GetComponent<SpriteRenderer>().enabled = true;
                break;
            case HazardLocation.BalloonRoom:
                firePosition = BalloonDeckHazardTransform.position;
                GameObject.Find("Balloon Deck Fire").GetComponent<SpriteRenderer>().enabled = true;
                break;
            default:
                firePosition = Vector3.negativeInfinity;
                break;
        }

        var hazardObject = Instantiate(FireHazardTemplate, firePosition, Quaternion.identity);
        var hazardScript = hazardObject.GetComponent<IShipHazard>();
        hazardScript.SetCoordinatorAndLocation(this, location);
        activeHazards.Add(hazardScript);
    }

    public void StartPumpMinigame()
    {
        audioManager.Play("Balloon Pop Hiss");
        AltimeterStation.TriggerBalloonDeflation(this, 35);
        activeHazards.Add(AltimeterStation);
    }

    public void StartBirdMinigame()
    {
        audioManager.Play("Bird Call");
        PunchGloveStation.TriggerBirdAttack(this, 50);
        activeHazards.Add(PunchGloveStation);
    }

    public void HazardWasResolved(IShipHazard hazard)
    {
        // return hazard location to available list
        availableHazardLocations.Add(hazard.Location);
        activeHazards.Remove(hazard);

        if (hazard.Location == HazardLocation.MainDeck)
        {
            GameObject.Find("Main Deck Fire").GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (hazard.Location == HazardLocation.EngineRoom)
        {
            GameObject.Find("Engine Room Fire").GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (hazard.Location == HazardLocation.BalloonRoom)
        {
            GameObject.Find("Balloon Deck Fire").GetComponent<SpriteRenderer>().enabled = false;
        }

        var ohLawdTheresAFire = activeHazards.Any(x => x.Location == HazardLocation.MainDeck || 
            x.Location == HazardLocation.EngineRoom || 
            x.Location == HazardLocation.BalloonRoom);

        if(!ohLawdTheresAFire)
        {
            audioManager.Stop("Fire Loop");
        }
    }

    public void HazardDestroyedTheShip()
    {
        GameOver(isWin: false);
    }

    public void GameOver(bool isWin)
    {
        var youFlewForThisLong = Time.time - sceneStartTime;
        GameValues.TimeInAir = youFlewForThisLong;
        GameValues.IsWin = isWin;
        SceneManager.LoadScene("GameOver");
    }
}
