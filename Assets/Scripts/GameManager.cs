using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        GameObject.DontDestroyOnLoad(GameObject.Instantiate(Resources.Load("GameManager")));
    }

    public static GameManager Instance;

    public event Action<GameStage> OnGameStageChanged;

    private GameStage currentStage = GameStage.Start;
    public GameStage Stage
    {
        get
        {
            return currentStage;
        }
        set
        {
            PreviousStage = currentStage;
            currentStage = value;
            OnGameStageChanged?.Invoke(value);
        }
    }

    public GameStage PreviousStage { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {      
        Stage = GameStage.Start;
    }


    public void Win()
    {
        Stage = GameStage.Win;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
