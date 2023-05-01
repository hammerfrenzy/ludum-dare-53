using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuWheelBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform
            .DORotate(new Vector3(0, 0, 70), 12)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
