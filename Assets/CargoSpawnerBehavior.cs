using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoSpawnerBehavior : MonoBehaviour
{
    
    public GameObject cargoPrefab;

    private CargoBehavior currentCargo = null;

    private float dropDuration = 2.5f;
    private float timer = 0;
    private bool isDropping = false;

    void Start()
    {
        Instantiate(cargoPrefab, new Vector3(-7.39f, -3.53f, 1.76f), Quaternion.identity);
        currentCargo = GameObject.FindObjectOfType<CargoBehavior>();

        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDropping) return;

        timer += Time.deltaTime;
        if (timer > dropDuration)
        {
            isDropping = false;
            timer = 0;

            currentCargo.Obliterate();
            Instantiate(cargoPrefab, new Vector3(-7.39f, -3.53f, 1.76f), Quaternion.identity);
            currentCargo = GameObject.FindObjectOfType<CargoBehavior>();
        }
    }

    public void MakeDelivery()
    {
        currentCargo.DropBoxes();
        isDropping = true;
    }
}
