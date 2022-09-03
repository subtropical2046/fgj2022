using CharacterControl;
using DG.Tweening;
using PlayerControl;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region Enum

    private enum State
    {
        Idle,
        Walking,
        Drunk,
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

    private State _state = State.Idle;
    private DrunkControlCore _drunkControlCore;

    #endregion

    private void Start()
    {
        _drunkControlCore =
            new DrunkControlCore(this, _data.DrunkControlData, OnDrunk);
        GameManager.Instance.OnGameStageChanged += OnGameStageChanged;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SmallObstacle"))
            OnFall();
    }

    private void OnGameStageChanged(GameStage gameStage)
    {
        switch (gameStage) {
            case GameStage.Play:
                ChangeState(State.Walking);
                _drunkControlCore.Start();
                break;
            case GameStage.Win:
            case GameStage.Lose:
                ChangeState(State.Idle);
                _drunkControlCore.Stop();
                break;
        }
    }

    private void OnDrunk(bool isDrunk)
    {
        ChangeState(isDrunk ? State.Drunk : State.Walking);
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
            .OnComplete(() => _state = State.Walking);
    }

    private void FixedUpdate()
    {
        if (_state != State.Walking)
            return;

        _rigidbody.MovePosition(
            _rigidbody.position +
            _characterControlManager.GetDeltaPosition(Time.deltaTime));
    }

    private void ChangeState(State state)
    {
        _state = state;
    }
}
