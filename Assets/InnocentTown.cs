using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InnocentTown : MonoBehaviour
{
    public bool infected = false;
    SpriteRenderer spriteRenderer;

    public GameObject skullObject;
    SpriteRenderer skullRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        skullRenderer = skullObject.GetComponent<SpriteRenderer>();
    }

    public void InfectTown()
    {
        infected = true;
        spriteRenderer.color = Color.green;
        skullRenderer.enabled = true;
    }
}
