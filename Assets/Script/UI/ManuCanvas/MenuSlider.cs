using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSlider : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider sfx;
    [SerializeField] private Slider music;

    void Awake()
    {

        sfx.onValueChanged.AddListener(OnValueChangeSfx);
        music.onValueChanged.AddListener(OnValueChangeMusic);
        
    }

    private void OnValueChangeMusic(float value)
    {
        AudioManager.Instance.UpdateMusicVol(value);
    }

    private void OnValueChangeSfx(float value)
    {
        AudioManager.Instance.UpdateSfxVol(value);

    }


}
