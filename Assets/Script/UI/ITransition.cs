using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransition
{
    // Start is called before the first frame update
    public abstract IEnumerator TransitionIn();
    public abstract IEnumerator TransitionOut();

}
