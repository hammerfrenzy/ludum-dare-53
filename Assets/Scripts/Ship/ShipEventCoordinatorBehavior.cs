using System.Collections;
using System.Collections.Generic;
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
    public PunchGloveStation PunchGloveStation;

    public Transform MainDeckHazardTransform;
    public Transform EngineRoomHazardTransform;
    public Transform BalloonDeckHazardTransform;

    public List<HazardAlertUIBehavior> alertUIBehaviors;

    // Would be cool to show "completion time"
    // time2speedrun
    private float sceneStartTime;

    private float timeToNextHazard = float.MaxValue;

    private List<HazardLocation> availableHazardLocations;
    private List<IShipHazard> activeHazards;

    private float defaultMinHazardTime = 20;
    private float defaultMaxHazardTime = 30;

    // Start is called before the first frame update
    void Start()
    {
        sceneStartTime = Time.time;

        activeHazards = new List<IShipHazard>();

        availableHazardLocations = new List<HazardLocation>
        {
            HazardLocation.Altimeter,
            HazardLocation.Bird,
            HazardLocation.BalloonRoom,
            HazardLocation.EngineRoom,
            HazardLocation.MainDeck,
        };

        // How frequently should hazards occur?
        timeToNextHazard = Random.Range(defaultMinHazardTime, defaultMaxHazardTime);
    }

    // Update is called once per frame
    void Update()
    {
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
        var location = availableHazardLocations[index];
        availableHazardLocations.RemoveAt(index);

        switch (location)
        {
            case HazardLocation.MainDeck:
            case HazardLocation.EngineRoom:
            case HazardLocation.BalloonRoom:
                StartAFire(location);
                break;
            case HazardLocation.Bird:
                StartBirdMinigame();
                break;
        }
        
        // TODO: Difficulty scaling over time?
        timeToNextHazard = Random.Range(defaultMinHazardTime, defaultMaxHazardTime);
    }

    public void StartAFire(HazardLocation location)
    {
        Vector3 firePosition;
        switch (location)
        {
            case HazardLocation.MainDeck:
                firePosition = MainDeckHazardTransform.position;
                break;
            case HazardLocation.EngineRoom:
                firePosition = EngineRoomHazardTransform.position;
                break;
            case HazardLocation.BalloonRoom:
                firePosition = BalloonDeckHazardTransform.position;
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

    public void StartBirdMinigame()
    {
        PunchGloveStation.TriggerBirdAttack(this, 50);
        activeHazards.Add(PunchGloveStation);
    }

    public void HazardWasResolved(IShipHazard hazard)
    {
        // return hazard location to available list
        availableHazardLocations.Add(hazard.Location);
        activeHazards.Remove(hazard);
    }

    public void HazardDestroyedTheShip()
    {
        var youFlewForThisLong = Time.time - sceneStartTime;
        GameValues.timeInAir = youFlewForThisLong;
        SceneManager.LoadScene("GameOver");
    }
}
