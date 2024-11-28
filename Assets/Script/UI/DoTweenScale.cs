using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoTweenScale : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RectTransform gameOverPanel;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float overshoot = 1.2f;

    private void OnEnable()
    {
        // Initially hide the Game Over panel
        gameOverPanel.localScale = Vector3.zero;
        ShowGameOver();
    }


    public void ShowGameOver()
    {
        // Scale from 0 to its original size with a bounce effect
        gameOverPanel.DOScale(Vector3.one, animationDuration)
            .SetEase(Ease.OutBack, overshoot) // Bounce effect
            .OnComplete(() =>
            {
                Debug.Log("Game Over UI Animation Complete!");
            });
    }
}
