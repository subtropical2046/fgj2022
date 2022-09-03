using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] GameObject counterUI = null;
    [SerializeField] Text timeCounterText = null;
    [SerializeField] LevelData levelData = null;

    bool isCounting = false;

    float timeRemaining = 0f;
    void Start()
    {
        GameManager.Instance.OnGameStageChanged += StartCounting;
        
    }

    private void StartCounting(GameStage stage)
    {
        timeRemaining = levelData.TimeLimit;
        isCounting = stage == GameStage.Play;
        counterUI.SetActive(isCounting);
    }

    private void Update()
    {
        

        if(isCounting)
        {
            timeRemaining -= Time.deltaTime;
            timeCounterText.text = Mathf.RoundToInt(timeRemaining).ToString();
        }

        if(timeRemaining <= 0)
        {
            GameManager.Instance.Stage = GameStage.Lose;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStageChanged -= StartCounting;
    }

}
