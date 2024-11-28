using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListiner : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    [SerializeField] UnityEvent response;
    private void OnEnable()
    {
        gameEvent.OnSubscribe(this);

    }
    void OnDisable()
    {
        gameEvent.OnUnSubscribe(this);
    }
    public void OnEventRaised()
    {
        response?.Invoke();
    }
}
