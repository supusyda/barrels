using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BarrelUIMenu : MonoBehaviour

{
    // Start is called before the first frame update
    Tween tween;
    RectTransform rectTransform;
    private Vector3 originalPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void Onlick()
    {
        Debug.Log(originalPosition);
        if (tween != null && tween.IsActive())
        {
            tween.Kill();
        }
        tween = rectTransform.DOShakeAnchorPos(1f, 100, 10, 0, false).OnComplete(() =>
        {
            rectTransform.DOLocalMove(originalPosition, .5f);
            // rectTransform.anchoredPosition = originalPosition;

        });
        if (tween.IsPlaying()) return;
        rectTransform.DOShakeRotation(1, 90);
        rectTransform.DOShakeScale(1);

    }
}
