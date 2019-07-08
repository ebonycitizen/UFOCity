using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chicken : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOShakeRotation(0.2f,50))
            .SetLoops(-1);     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
