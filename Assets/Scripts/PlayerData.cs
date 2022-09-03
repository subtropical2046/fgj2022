using DrunkControl;
using FallControl;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private DrunkControlData _drunkControlData;
    [SerializeField]
    private FallControlData _fallControlData;

    public DrunkControlData DrunkControlData => _drunkControlData;
    public FallControlData FallControlData => _fallControlData;
}
