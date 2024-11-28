using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform gameOverPanel;
    [SerializeField] Transform gameOverHolder;
    void OnEnable()
    {
        Event.OnGameOver.AddListener(ShowGameOver);
    }

    public void ShowGameOver()
    {
        gameOverPanel.gameObject.SetActive(true);
        gameOverHolder.gameObject.SetActive(true);
    }


}
