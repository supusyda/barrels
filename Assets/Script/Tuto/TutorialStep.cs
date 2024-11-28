using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public abstract class TutorialStep : MonoBehaviour
{
    protected Transform ojbShow;


    public abstract void Activate();


    public abstract void Deactivate();


    public abstract bool IsStepComplete();

}
