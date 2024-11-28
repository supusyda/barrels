using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    public TutorialStep[] tutorialSteps; // Steps in the tutorial

    private int currentStepIndex = 0;

    void Start()
    {
        if (tutorialSteps.Length > 0)
        {
            StartStep(0); // Start the first step
        }
    }
    void Update()
    {
        if (tutorialSteps.Length == 0) return;

        // Check if the current step is complete
        if (tutorialSteps[currentStepIndex].IsStepComplete())
        {
            CompleteStep();
        }
    }
    public void StartStep(int index)
    {
        if (index < 0 || index >= tutorialSteps.Length) return;

        // Deactivate the current step if it exists

        currentStepIndex = index;
        tutorialSteps[currentStepIndex].Activate();
    }

    public void CompleteStep()
    {
        tutorialSteps[currentStepIndex].Deactivate();
        if (currentStepIndex < tutorialSteps.Length - 1)
        {
            StartStep(currentStepIndex + 1);
        }
        else
        {
            Debug.Log("Tutorial Complete!");
        }
    }
}
