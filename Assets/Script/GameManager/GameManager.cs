using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
enum GameState
{
    None,
    EditGridState,
    PlayMode,
    SpawnBlock,
    WaitForInput,
    LostGame,
    WinGame,
}
public enum SwipeDirection
{
    None,
    Up,
    Down,
    Left,
    Right,
}
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public bool isEditMode = true;
    private GameState _gameState = GameState.None;
    [SerializeField] private int numberOfYesNode = 0;

    private int currentOnYesNode;
    private int currentOnNoNode;
    private int currentBarrelHasCheck = 0;
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void OnEnable()
    {
        Init();

        if (GameManager.instance._gameState != GameState.PlayMode) return;
        AddListener();
        Debug.Log("ADD LISTENER");
    }
    void AddListener()
    {
        NodeEvent.OnInteractNoNode.AddListener(OnInteractNoNode);
        NodeEvent.OnInteractYesNode.AddListener(OnInteractYesNode);
        Event.OnWinLevel.AddListener(OnWinLevel);
        Event.OnDoneMoveBarrel.AddListener(CheckForWin);

    }

    private void OnWinLevel()
    {
        ResetYesNoNum();
    }
    void ResetYesNoNum()
    {
        currentOnYesNode = 0;
        currentOnNoNode = 0;
        UIEvent.OnUIUpdateYesNo.Invoke(currentOnYesNode, currentOnNoNode);
    }

    void OnDisable()
    {

        NodeEvent.OnInteractNoNode.RemoveListener(OnInteractNoNode);
        NodeEvent.OnInteractYesNode.RemoveListener(OnInteractYesNode);
    }
    private void OnInteractYesNode(int amount)
    {

        currentOnYesNode += amount;
        UIEvent.OnUIUpdateYesNo.Invoke(currentOnYesNode, currentOnNoNode);


    }
    void CheckForWin()
    {
        if (CanWin())
        {


            Event.OnWinLevel.Invoke();

        }
    }
    private void OnInteractNoNode(int amount)
    {
        currentOnNoNode += amount;
        Debug.Log("amount" + amount);
        UIEvent.OnUIUpdateYesNo.Invoke(currentOnYesNode, currentOnNoNode);

    }

    private bool IsAllBarrelDoneMove()
    {
        return currentBarrelHasCheck >= GridEditManager.instance.GetBarrelNum();
    }
    public void SetWinCodition(int yesNodeAmount)
    {
        if (yesNodeAmount < 0) return;
        numberOfYesNode = yesNodeAmount;
    }
    bool CanWin()
    {


        return currentOnYesNode == numberOfYesNode && currentOnNoNode == 0;
    }
    public int GetWinCondition()
    {
        return numberOfYesNode;
    }
    void ChangeGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.EditGridState:
                // GenGrid();
                GridEditManager.OnEditGirdSelected.Invoke();
                break;
            case GameState.PlayMode:
                Debug.Log("PLAY MODE");

                LevelManager.Instance.LoadLevel(LevelManager.Instance.GetCurrentLevel());

                break;
            case GameState.SpawnBlock:
                // SpawnBlock(10);

                break;
            case GameState.WaitForInput:
                break;
            case GameState.LostGame:
                break;
            case GameState.WinGame:
                break;

            default:
                break;
        }
        _gameState = newState;
    }
    GameState GetGameState()
    {
        return _gameState;
    }



    private void Start()
    {
    }
    private void Update()
    {

        if (_gameState != GameState.WaitForInput)
            return;

    }
    void Init()
    {
        if (!isEditMode) { ChangeGameState(GameState.PlayMode); return; };
        ChangeGameState(GameState.EditGridState);
        Debug.Log("EDIT MODE ?");
    }
    void SetCamPos(Vector3 newPos)
    {
        Camera.main.transform.position = newPos;
    }



}
