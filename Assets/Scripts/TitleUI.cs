using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] Text playBtnText = null;
    [SerializeField] Text exitBtnText = null;

    [SerializeField] Text titleText = null;


    private void Start()
    {
        GameManager.Instance.OnGameStageChanged += ChangeStage;
    }

    private void ChangeStage(GameStage stage)
    {
        gameObject.SetActive(stage != GameStage.Play);
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
