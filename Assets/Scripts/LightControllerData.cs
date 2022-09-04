using UnityEngine;

[CreateAssetMenu(menuName = "Data/LightControllerData Data")]
public class LightControllerData : ScriptableObject
{
    [SerializeField] private bool _tunrOnOffRandomMove = default;
    [SerializeField] private float _decideRandomMoveInterval = default;
    [SerializeField] private int _randomMoveRate = default;
    [SerializeField] private float _randomMoveSpeed = default;
    [SerializeField] private float _attractedMoveSpeed = default;

    public bool TunrOnOffRandomMove => _tunrOnOffRandomMove;
    public float DecideRandomMoveInterval => _decideRandomMoveInterval;
    public int RandomMoveRate => _randomMoveRate;
    public float RandomMoveSpeed => _randomMoveSpeed;
    public float AttractedMoveSpeed => _attractedMoveSpeed;
}
