using DrunkControl;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private DrunkControlData _drunkControlData;

    public DrunkControlData DrunkControlData => _drunkControlData;
}
