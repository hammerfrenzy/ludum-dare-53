using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRatBehavior : MonoBehaviour
{
    private float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        var currentPos = transform.position;
        var time = Random.Range(2, 3);
        var bounceAmount = Random.Range(1, 3);

        transform
            .DOLocalMoveY(currentPos.y - bounceAmount, time)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad)
            .SetDelay(Random.Range(0, 1f));

        rotateSpeed = Random.Range(-40, 40);

        GetComponent<Animator>().SetBool("isInteracting", true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
