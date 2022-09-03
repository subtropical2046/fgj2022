using System;
using CharacterControl;
using DG.Tweening;
using DrunkControl;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region Enum

    private enum State
    {
        Normal,
        IsDrunk,
        Fall
    }

    #endregion

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

    private State _state = State.Normal;
    private DrunkControlCore _drunkControlCore;

    #endregion

    private void Start()
    {
        _drunkControlCore = new DrunkControlCore(_data.DrunkControlData, OnDrunk);
        _drunkControlCore.Start(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SmallObstacle"))
            OnFall();
    }

    private void OnDrunk(bool isDrunk)
    {
        _state = isDrunk ? State.IsDrunk : State.Normal;
        // TODO Change to animation control
        _spriteRenderer.DOColor(isDrunk ? Color.red : Color.white, 0.2f);
    }

    private void OnFall()
    {
        _state = State.Fall;

        var curPosition = _rigidbody.position;

        DOTween.Sequence()
            .Append(
                _rigidbody.DOMove(
                    curPosition +
                    _characterControlManager.MovingDirection *
                    _data.FallControlData.FallDistance,
                    _data.FallControlData.FallPeriod))
            .AppendInterval(_data.FallControlData.FallIdlePeriod)
            .OnComplete(() => _state = State.Normal);
    }

    private void FixedUpdate()
    {
        if (_state != State.Normal)
            return;

        _rigidbody.MovePosition(
            _rigidbody.position +
            _characterControlManager.GetDeltaPosition(Time.deltaTime));
    }
}
