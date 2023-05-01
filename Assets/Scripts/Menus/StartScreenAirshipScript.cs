using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenAirshipScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform
            .DOMoveY(transform.position.y - 20, 15)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);

        transform
            .DOMoveX(transform.position.x - 4, 20)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
    }
}
