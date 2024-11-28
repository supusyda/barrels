using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddBarrelBtn : MonoBehaviour
{
    public static UnityEvent addBoxBtnPress = new();
    Button button;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(buttonPress);
    }
    void buttonPress()
    {
        addBoxBtnPress.Invoke();
    }
    // Start is called before the first frame update

}
