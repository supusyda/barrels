using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBackBtn : MonoBehaviour
{
    [SerializeField] Transform hideTarget;
    [SerializeField] Transform showTarget;

    Button button;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        AudioManager.Instance.SaveMixer();
        hideTarget.gameObject.SetActive(false);
        showTarget.gameObject.SetActive(true);
    }
}
