using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundSettingOpenBtn : Btnbase
{
    // Start is called before the first frame update
    [SerializeField] Transform hideTarget;
    [SerializeField] GameObject showTarget;
    protected override void OnButtonClick()
    {
        if (hideTarget != null) hideTarget.gameObject.SetActive(false);
        if (showTarget != null) showTarget.gameObject.SetActive(true);
    }
}
