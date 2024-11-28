using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenBtn : MonoBehaviour
{
    // Start is called before the first frame update
    Button button;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        AudioManager.Instance.PlaySound(AudioName.OnMouseClick);
        transform.DOShakeScale(1, .5f).OnComplete(() =>
        {
            LevelManager.Instance.TransitionToScene("LevelSelect", "Circle");

        });

    }
}
