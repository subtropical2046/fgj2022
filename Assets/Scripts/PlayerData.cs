using PlayerControl;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private DrunkControlData _drunkControlData;
    [SerializeField]
    private FallControlData _fallControlData;
    [SerializeField]
    private StuckControlData _stuckControlData;

    public DrunkControlData DrunkControlData => _drunkControlData;
    public FallControlData FallControlData => _fallControlData;
    public StuckControlData StuckControlData => _stuckControlData;
}
