using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoaderManager : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField] Transform transition;
    // const float fadeDuration = 1f;
    // const float targetScale = 30f;

    // void OnEnable()
    // {
    //     Event.OnWinLevel.AddListener(OnWinLevel);
    // }
    // void OnDisable()
    // {
    //     Event.OnWinLevel.RemoveListener(OnWinLevel);
    // }

    // private void OnWinLevel()
    // {
    //     FadeIn();
    // }
    // private void FadeIn()
    // {
    //     transition.gameObject.SetActive(true);
    //     transition.localScale = Vector3.zero;
    //     // transition.GetComponent<Image>().color = new Color(0, 0, 0, 1);  // Start fully opaque

    //     // transition..GetComponent<Image>().DOFade(0, fadeDuration).OnComplete(() => fadePanel.gameObject.SetActive(false));
    //     transition.DOScale(37, fadeDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
    //     {
    //         LevelManager.Instance.LoadNextLevel();
    //         FadeOut();
    //     });
    //     // 
    // }
    // private void FadeOut()
    // {
    //     // transition.localScale = new Vector3(targetScale, targetScale, targetScale);
    //     transition.DOScale(0, fadeDuration).SetEase(Ease.InOutQuad).OnComplete(() => transition.gameObject.SetActive(false));
    // }
}
