using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class UIEvent
{
    // Start is called before the first frame updated
    public static UnityEvent<int, int> OnUIUpdateYesNo = new();

}
