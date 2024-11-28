using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwpieTutoStep : TutorialStep
{


    // Start is called before the first frame update
    bool isDoneStep = false;
    void OnEnable()
    {
        Event.OnDoneMoveBarrel.AddListener(OnDoneMove);
    }
    void OnDisable()
    {
        Event.OnDoneMoveBarrel.RemoveListener(OnDoneMove);
    }
    void OnDoneMove()
    {
        isDoneStep = true;
    }
    public override void Activate()
    {
        transform.gameObject.SetActive(true);
    }

    public override void Deactivate()
    {
        transform.gameObject.SetActive(false);

    }

    public override bool IsStepComplete()
    {
        return isDoneStep;
    }
}
