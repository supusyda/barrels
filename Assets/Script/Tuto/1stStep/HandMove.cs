using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandMove : MonoBehaviour
{
    Vector3 startPos;
    RectTransform rectTransform;

    void Awake()
    {
        startPos = this.transform.GetComponent<RectTransform>().anchoredPosition;
        rectTransform = transform.GetComponent<RectTransform>();
        rectTransform.DOAnchorPos(startPos + Vector3.right * 200, 1)
            .OnComplete(() =>
            {
                // Return to the starting position
                rectTransform.DOAnchorPos(startPos, 1);
            }).SetLoops(-1, LoopType.Yoyo);
    }
}
