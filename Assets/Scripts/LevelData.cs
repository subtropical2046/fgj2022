using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] float _timeLimit = 10f;
    [SerializeField] GameObject _levelPrefab = null;
    
    public float TimeLimit => _timeLimit;

    public GameObject LevelPrefab => _levelPrefab;
}
