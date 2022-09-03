using CharacterControl;
using DG.Tweening;
using DrunkControl;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private PlayerData _data;
    [SerializeField]
    private CharacterControlManager _characterControlManager;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    #endregion

    #region Private Members

    private DrunkControlCore _drunkControlCore;

    #endregion

    private void Start()
    {
        _drunkControlCore = new DrunkControlCore(_data.DrunkControlData, OnDrunk);
        _drunkControlCore.Start(this);
    }

    private void OnDrunk(bool isDrunk)
    {
        // TODO Change to animation control
        _spriteRenderer.DOColor(isDrunk ? Color.red : Color.white, 0.2f);
    }

    private void FixedUpdate()
    {
        if (_drunkControlCore.IsDrunk)
            return;

        _rigidbody.MovePosition(
            _rigidbody.position +
            _characterControlManager.GetDeltaPosition(Time.deltaTime));
    }
}
