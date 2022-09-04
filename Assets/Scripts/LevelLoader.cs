using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] LevelData[] _levelDatas = null;
    [SerializeField] bool _isRandom = true;

    private int levelIndex = 0;
    private void Awake()
    {
        GameManager.Instance.OnGameStageChanged += StageChanged;
    }

    private void StageChanged(GameStage stage)
    {
        if (stage != GameStage.Start)
            return;

        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        if(_isRandom)
        {
            levelIndex = UnityEngine.Random.Range(0, _levelDatas.Length);
        }
        else if (GameManager.Instance.PreviousStage == GameStage.Win)
        {
            levelIndex++;
            if (levelIndex >= _levelDatas.Length)
                levelIndex = 0;
        }
        

        Instantiate(_levelDatas[levelIndex].LevelPrefab, transform);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStageChanged -= StageChanged;
    }
}
