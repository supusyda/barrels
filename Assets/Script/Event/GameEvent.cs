using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public HashSet<GameEventListiner> gameEventListiners { get; private set; } = new HashSet<GameEventListiner>();
    public void OnSubscribe(GameEventListiner gameEventListiner)
    {
        gameEventListiners.Add(gameEventListiner);
    }
    public void OnUnSubscribe(GameEventListiner gameEventListiner)
    {
        gameEventListiners.Remove(gameEventListiner);
    }
    public void BroadCast()
    {
        foreach (GameEventListiner gameEventListiner in gameEventListiners)
        {
            gameEventListiner.OnEventRaised();
        }
    }


}
