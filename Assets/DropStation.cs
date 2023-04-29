using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropStation : MonoBehaviour
{
    private bool isInteractable;
    public SpriteRenderer steeringUi;

    // Start is called before the first frame update
    void Start()
    {
        steeringUi = GameObject.Find("ExclamationMarkUI").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        steeringUi.GetComponent<SpriteRenderer>().enabled = isInteractable;
    }

    public void SetInteracting(bool interacting)
    {
        isInteractable = interacting;
    }
}
