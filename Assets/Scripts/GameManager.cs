using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
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
            currentStage = value;
            OnGameStageChanged?.Invoke(value);
        }
    }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {      
        Stage = GameStage.Start;
    }


    public void CheatWin()
    {
        Stage = GameStage.Win;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
