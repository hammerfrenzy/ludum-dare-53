using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoBehavior : MonoBehaviour
{
    Rigidbody[] boxes;

    void Start()
    {
        boxes = GetComponentsInChildren<Rigidbody>();
        foreach (var box in boxes)
        {
            box.isKinematic = true;
        }

        transform.DOMove(new Vector3(-1.79f, -8.13f, 1.73f), 0.5f);
    }

    public void DropBoxes()
    {
        foreach (var box in boxes)
        {
            box.isKinematic = false;
        }
    }

    public void Obliterate()
    {
        Destroy(gameObject);
    }
}
