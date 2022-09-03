using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] GameObject timeCounter = null;
    [SerializeField] Text timeCounterText = null;

    [SerializeField] float timeLimit = 120f;

    float timeRemaining = 0f;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {      
        Stage = GameStage.Start;
    }

    private void Update()
    {
        if(Stage != GameStage.Play)
        {
            timeCounter.gameObject.SetActive(false);
            return;
        }

        if(!timeCounter.gameObject.activeSelf)
        {
            timeCounter.gameObject.SetActive(true);
        }

        timeRemaining -= Time.deltaTime;
        timeCounterText.text = Mathf.RoundToInt(timeRemaining).ToString();

        if(timeRemaining <= 0f)
        {
            Stage = GameStage.Lose;
        }
    }

    public void CheatWin()
    {
        Stage = GameStage.Win;
    }


    #region ButtonCall
    public void PlayGame()
    {
        timeRemaining = timeLimit;
        Stage = GameStage.Play;
    }

    public void ExitGame()
    {
        if (Stage == GameStage.Start)
            Application.Quit();
        else
            Stage = GameStage.Start;
    }
    #endregion
}
