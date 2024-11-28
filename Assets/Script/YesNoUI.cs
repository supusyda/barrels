using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YesNoUI : MonoBehaviour
{
    [SerializeField] TMP_Text yesText;
    [SerializeField] TMP_Text noText;
    void OnEnable()
    {
        UIEvent.OnUIUpdateYesNo.AddListener(SetYesNoNodeScore);
    }
    void OnDisable()
    {
        UIEvent.OnUIUpdateYesNo.RemoveListener(SetYesNoNodeScore);
    }
    void SetYesNoNodeScore(int yesNum, int noNum)
    {
        yesText.text = yesNum.ToString();
        noText.text = noNum.ToString();
    }
}
