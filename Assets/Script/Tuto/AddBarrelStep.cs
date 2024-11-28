using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBarrelStep : TutorialStep
{


    // Start is called before the first frame update
    bool isDoneStep = false;
    void OnEnable()
    {
        Event.OnDoneSpawnBarrel.AddListener(OnDoneSpawnBarrel);
    }

    private void OnDoneSpawnBarrel()
    {
        isDoneStep = true;
    }

    void OnDisable()
    {
        Event.OnDoneMoveBarrel.RemoveListener(OnDoneSpawnBarrel);
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
