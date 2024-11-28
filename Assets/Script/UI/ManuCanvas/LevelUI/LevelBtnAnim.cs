using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LevelBtnAnim : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform lockImage;
    [SerializeField] TMP_Text levelTxt;

    void Awake()
    {
        levelTxt = GetComponentInChildren<TMP_Text>();

    }
    public void PlayIsLockAnim()
    {
        lockImage.GetComponent<RectTransform>().DOShakeAnchorPos(1, 10);
    }
    public void lockEnable()
    {
        lockImage.gameObject.SetActive(true);
    }
    public void lockDisable()
    {
        lockImage.gameObject.SetActive(false);
    }
    public void SetLvlTxt(string txt)
    {
        levelTxt.text = txt;
    }
    public Tween OnClickAnim()
    {
        return transform.DOShakeScale(1, 1).SetEase(Ease.OutQuad);
    }
}
