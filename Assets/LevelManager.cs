using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    const string Current_Level = "Current_Level";
    public static LevelManager Instance { get; private set; }
    private int currentLevel = 1;

    private List<string> levelFiles;
    [SerializeField] List<Transform> transitions;

    private void Awake()
    {
        // Ensure only one instance of LevelManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep across scene loads
        }
        else
        {
            Destroy(gameObject);
        }

        // Load all level files at the start
        // transitions

        LoadTransition();
        LoadAllLevelFiles();

    }
    void OnEnable() => AddListener();

    void OnDisable() => RemoveListener();

    void AddListener()
    {
        Event.OnWinLevel.AddListener(OnWin);
        BackBtnUI.OnClickBackBtn.AddListener(OnClickBackBtn);
    }
    void RemoveListener()
    {
        Event.OnWinLevel.RemoveListener(OnWin);
    }
    void LoadTransition()
    {
        Transform allTransition = transform.Find("Transition");
        foreach (Transform transition in allTransition)
        {
            transitions.Add(transition);
        }
    }
    ITransition GetTransitionByName(string name)
    {
        Debug.Log(transitions.First(t => t.name == name).name);
        return transitions.First(t => t.name == name).GetComponent<ITransition>();
    }
    // Load all level files ending with level.json
    private void LoadAllLevelFiles()
    {

        // levelFiles = HandleSave.GetAllSaveFile().ToList<>;
        // foreach (string file in files)
        // {
        //     levelFiles.Add(Path.GetFileNameWithoutExtension(file));
        // }
    }

    // Get all available levels
    public List<string> GetAvailableLevels()
    {
        return new List<string>(levelFiles);
    }

    // Load a level by name


    // Apply loaded level data to the scene


    // Transition to a Unity scene by index or name
    public void TransitionToScene(string sceneName, string transitionName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, transitionName));
    }

    private IEnumerator LoadSceneAsync(string sceneName, string transitionName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        ITransition transition = GetTransitionByName(transitionName);
        Debug.Log(transition);
        // Optionally, show a loading screen or progress here
        yield return transition.TransitionIn();
        while (asyncLoad.progress < 0.9f)
        {

            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        // yield return new WaitUntil(() => asyncLoad.isDone);
        yield return transition.TransitionOut();
        SwipeDetector.OnLockSwipe.Invoke(false);//set lock swipe to false
    }
    IEnumerator NextLevelAnim(string transitionName)
    {
        ITransition transition = GetTransitionByName(transitionName);
        Debug.Log(GetTransitionByName(transitionName));
        yield return transition.TransitionIn();
        LoadNextLevel();
        yield return transition.TransitionOut();
        SwipeDetector.OnLockSwipe.Invoke(false);//set lock swipe to false

    }
    // Save level progress (customize as needed)
    public void SetCurrentLevel(int level)
    {
        currentLevel = level;

    }
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public void LoadNextLevel()
    {
        currentLevel += 1;
        if (!HandleSave.HasLevel(currentLevel))

        {
            Event.OnGameOver.Invoke();
            return;
        }
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int level)
    {
        GridEditManager.OnLoadPlayLevel.Invoke(level);
        CommandScheduler.Clear();
    }
    public void LoadLevelFormMenu(int level)
    {
        // TransitionToScene("Game2");
        SetCurrentLevel(level);
        TransitionToScene("Game2", "Circle");

    }
    void OnWin()
    {
        SaveCurrentLevelToPlayerPref();
        if (!HandleSave.HasLevel(currentLevel + 1))
        {
            Event.OnGameOver.Invoke();
            return;
        }
        StartCoroutine(NextLevelAnim("Circle"));
    }
    void OnClickBackBtn()
    {
        TransitionToScene("Menu", "Circle");
    }
    void SaveCurrentLevelToPlayerPref()
    {
        if (LoadCurrentLevelFormPlayerPref() > currentLevel) return;
        PlayerPrefs.SetInt("Current_Level", currentLevel);
        PlayerPrefs.Save(); // Ensure the changes are written to storage.
        Debug.Log("Level saved: " + currentLevel);
    }
    public int LoadCurrentLevelFormPlayerPref()
    {
        int level = PlayerPrefs.GetInt(Current_Level, 1); // Default to level 1 if not set.

        return level;
    }
}
