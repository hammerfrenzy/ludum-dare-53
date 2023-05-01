using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryRatBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool("isInteracting", true);

        transform
            .DOJump(transform.position + Vector3.left, 1.5f, 1, 1)
            .SetDelay(Random.Range(0, 1f))
            .SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
