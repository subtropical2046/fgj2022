using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] GameObject mainUI = null;
    [SerializeField] Button playBtn = null;
    [SerializeField] Text playBtnText = null;
    [SerializeField] Button endBtn = null;
    [SerializeField] Text exitBtnText = null;

    [SerializeField] Text titleText = null;
    [SerializeField] GameObject mapQR = null;

    private void Start()
    {
        mainUI.SetActive(true);
        GameManager.Instance.OnGameStageChanged += ChangeStage;
        playBtn.onClick.AddListener(PlayGame);
        endBtn.onClick.AddListener(EndGame);
    }

    private void PlayGame()
    {
        if (GameManager.Instance.Stage == GameStage.Start)
        {
            GameManager.Instance.Stage = GameStage.Play;
        }
        else
        {
            //GameManager.Instance.Stage = GameStage.Start;
            GameManager.Instance.ReloadScene();
        }
    }

    private void EndGame()
    {
        if(GameManager.Instance.Stage == GameStage.Start)
        {
            Application.Quit();
        }
        else
        {
            GameManager.Instance.ReloadScene();
        }
        
    }



    private void ChangeStage(GameStage stage)
    {
        gameObject.SetActive(stage != GameStage.Play);
        mapQR.SetActive(stage == GameStage.Start);
        switch (stage)
        {
            case GameStage.Start:
                playBtnText.text = "Play";
                exitBtnText.text = "Exit";
                titleText.text = "社畜的深夜運動";
                titleText.color = Color.white;
                break;
            case GameStage.Play:
                
                break;
            case GameStage.Lose:
                playBtnText.text = "Restart";
                exitBtnText.text = "MainMenu";
                titleText.text = "You Lose";
                titleText.color = Color.red;
                break;
            case GameStage.Win:
                playBtnText.text = "Restart";
                exitBtnText.text = "MainMenu";
                titleText.text = "You Win";
                titleText.color = Color.green;
                break;
        }
    }


    private void OnDestroy()
    {
        GameManager.Instance.OnGameStageChanged -= ChangeStage;
    }


}
