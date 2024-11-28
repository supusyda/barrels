using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CircleTrans : MonoBehaviour, ITransition
{
    // Start is called before the first frame update
    public float fadeDuration = 1f;

    public void TransitionIn()
    {
        FadeIn();
    }
    public void TransitionOut()
    {
        FadeOut();
    }
    private Tween FadeIn()
    {
        transform.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        // transition.GetComponent<Image>().color = new Color(0, 0, 0, 1);  // Start fully opaque

        // transition..GetComponent<Image>().DOFade(0, fadeDuration).OnComplete(() => fadePanel.gameObject.SetActive(false));
        return transform.DOScale(37, fadeDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
          {
              //   LevelManager.Instance.LoadNextLevel();
          });
        // 
    }
    private Tween FadeOut()
    {
        // transition.localScale = new Vector3(targetScale, targetScale, targetScale);
        return transform.DOScale(0, fadeDuration).SetEase(Ease.InOutQuad).OnComplete(() => transform.gameObject.SetActive(false));
    }

    IEnumerator ITransition.TransitionIn()
    {
        yield return FadeIn().WaitForCompletion();
    }

    IEnumerator ITransition.TransitionOut()
    {
        yield return FadeOut().WaitForCompletion();
    }
}
