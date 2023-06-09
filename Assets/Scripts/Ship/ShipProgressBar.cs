using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipProgressBar : MonoBehaviour
{
    public bool Invert = false;

    private float totalTime = 0;
    private float timeToFill = 0;
    private bool shouldProgress = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldProgress) { return; }

        totalTime += Time.deltaTime;

        UpdateScale();
    }

    private void UpdateScale()
    {
        var progress = totalTime / timeToFill;
        progress = Mathf.Clamp01(progress);

        if (Invert) { progress = 1 - progress; }

        transform.localScale = new Vector3(progress, 1, 1);
    }

    public void SetTimeToFill(float time)
    {
        totalTime = 0;
        timeToFill = time;
        UpdateScale();
    }

    public void SetMakeProgress(bool shouldProgress)
    {
        this.shouldProgress = shouldProgress;
    }

    public void ManuallyProgress(float amount)
    {
        totalTime += amount;
        UpdateScale();
    }
}
