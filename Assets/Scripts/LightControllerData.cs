using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/LightControllerData Data")]
public class LightControllerData : ScriptableObject
{
    public bool _tunrOnOffRandomMove = default;
    public float _decideRandomMoveInterval = default;
    public int _randomMoveRate = default;
    public float _randomMoveSpeed = default;
    public float _attractedMoveSpeed = default;
}
