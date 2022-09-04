using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    [SerializeField] Transform _player = null;
    [SerializeField] Transform _light = null;
    void Awake()
    {
        GameManager.Instance.OnGameStageChanged += StageChanged;
    }

    private void StageChanged(GameStage stage)
    {
        
        if(stage == GameStage.Start)
        {
            Vector2 pos = transform.GetChild(Random.Range(0, transform.childCount)).position;
            _player.position = pos;
            _light.position = pos;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStageChanged -= StageChanged;
    }
}
