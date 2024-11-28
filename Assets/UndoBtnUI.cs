using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoBtnUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Button button;
    private float _timeBetweenClick = .5f;
    private float _timer = .5f;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (_timer < _timeBetweenClick) return;
            _timer = 0;
            CommandScheduler.Undo();
        });
    }
    void Update()
    {
        _timer += Time.deltaTime;
    }
}
