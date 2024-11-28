using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] RectTransform[] rectTransforms;
    [SerializeField] RectTransform PlayBtn;
    Vector3[] targetPos;

    [SerializeField] RectTransform canvas;
    [SerializeField] Slider music;
    [SerializeField] Slider sfx;
    Camera mainCamera;





    private void Start()
    {
        mainCamera = Camera.main;
        targetPos = new Vector3[rectTransforms.Length];
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            targetPos[i] = rectTransforms[i].anchoredPosition;
        }
        MoveObjectsToStart();
        ShowPlayBtn();
        LoadMixer();
        AudioManager.Instance.PlayMusic("MenuMusic");
    }

    private void MoveObjectsToStart()
    {
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            Vector2 randomOffScreenPos = GetRandomOffScreenPosition();
            Vector3 targetPos = this.targetPos[i];
            RectTransform rectTransform = rectTransforms[i];


            // Place the object at the random off-screen position
            rectTransform.anchoredPosition = randomOffScreenPos;

            // Animate the object to its start position
            rectTransform.DOAnchorPos(targetPos, 1.5f).SetEase(Ease.InOutQuad);
        }

    }

    private Vector2 GetRandomOffScreenPosition()
    {
        Vector2 canvasSize = canvas.sizeDelta;

        float x = 0, y = 0;

        // Randomize which edge of the screen (top, bottom, left, right)
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: // Top
                x = Random.Range(-canvasSize.x / 2, canvasSize.x / 2);
                y = canvasSize.y / 2 + 100; // Slightly above the top
                break;
            case 1: // Bottom
                x = Random.Range(-canvasSize.x / 2, canvasSize.x / 2);
                y = -canvasSize.y / 2 - 100; // Slightly below the bottom
                break;
            case 2: // Left
                x = -canvasSize.x / 2 - 100; // Slightly left of the screen
                y = Random.Range(-canvasSize.y / 2, canvasSize.y / 2);
                break;
            case 3: // Right
                x = canvasSize.x / 2 + 100; // Slightly right of the screen
                y = Random.Range(-canvasSize.y / 2, canvasSize.y / 2);
                break;
        }

        return new Vector2(x, y);
    }
    void ShowPlayBtn()
    {
        Vector2 canvasSize = canvas.sizeDelta;
        Vector2 playBtnTargetPos = PlayBtn.anchoredPosition;
        float y = canvasSize.y / 2 + 200;
        PlayBtn.anchoredPosition = new Vector2(playBtnTargetPos.x, y);
        PlayBtn.DOAnchorPos(playBtnTargetPos, 1.5f).SetEase(Ease.InOutQuad);
    }
    public void LoadMixer()
    {
        float musicVol = PlayerPrefs.GetFloat("musicVol", 1);
        float sfxVol = PlayerPrefs.GetFloat("sfxVol", 1);
        music.value = musicVol;
        sfx.value = sfxVol;

    }
}
