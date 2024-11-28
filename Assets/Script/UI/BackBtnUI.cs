using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackBtnUI : Btnbase
{
    // Start is called before the first frame update
    public static UnityEvent OnClickBackBtn = new();
    protected override void OnButtonClick()
    {
        OnClickBackBtn.Invoke();
    }
}
