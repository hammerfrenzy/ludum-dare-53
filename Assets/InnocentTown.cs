using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnocentTown : MonoBehaviour
{
    public bool infected = false;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void InfectTown()
    {
        infected = true;
        spriteRenderer.color = Color.green;
    }
}
