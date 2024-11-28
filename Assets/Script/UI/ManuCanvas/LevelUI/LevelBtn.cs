using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class LevelBtn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int level;
    [SerializeField] bool isUnlock;
    private LevelBtnAnim _levelBtnAnim;


    Button button;
    void Awake()
    {
        button = GetComponent<Button>();
        _levelBtnAnim = GetComponent<LevelBtnAnim>();
        button.onClick.AddListener(() =>
        {
            buttonPress();
        });
    }
    void Start()
    {
        CheckThisLevelIsUnlock();
        _levelBtnAnim.SetLvlTxt(level.ToString());

    }
    private void CheckThisLevelIsUnlock()
    {
        if (level <= LevelManager.Instance.LoadCurrentLevelFormPlayerPref())
        {
            //level is unlocked
            isUnlock = true;
            _levelBtnAnim.lockDisable();
        }
        else
        {
            //level is not unlocked
            isUnlock = false;

            _levelBtnAnim.lockEnable();
        }
    }
    public void buttonPress()
    {
        AudioManager.Instance.PlaySound("OnClickLevel");
        if (isUnlock == false)
        {
            _levelBtnAnim.PlayIsLockAnim();
        }
        else
        {
            _levelBtnAnim.OnClickAnim().OnComplete(() =>
            {

                LevelManager.Instance.LoadLevelFormMenu(level);
                AudioManager.Instance.PlayMusic("GameMusic");
            });
        }
    }

}
