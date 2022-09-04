using CharacterControl;
using DG.Tweening;
using PlayerControl;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IPlayerSpriteControlTarget
{
    #region Enum

    public enum State
    {
        Idle,
        Walking,
        Drunk,
        Fall,
        Stuck
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
    private Animator _animator;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    #endregion

    #region Private Members

    private State _state = State.Idle;
    private PlayerSpriteControlCore _spriteControlCore;
    private DrunkControlCore _drunkControlCore;

    #endregion

    private void Start()
    {
        _spriteControlCore =
            new PlayerSpriteControlCore(this, _animator);
        _drunkControlCore =
            new DrunkControlCore(this, _data.DrunkControlData, OnDrunk);
        GameManager.Instance.OnGameStageChanged += OnGameStageChanged;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SmallObstacle"))
            OnFall();
        else if (other.CompareTag("Attractor"))
            OnAttracted();
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
        if ((isDrunk && _state != State.Walking)
            || (!isDrunk && _state != State.Drunk))
            return;

        ChangeState(isDrunk ? State.Drunk : State.Walking);
        // TODO Change to animation control
        _spriteRenderer.DOColor(isDrunk ? Color.red : Color.white, 0.2f);
    }

    private void OnFall()
    {
        ChangeState(State.Fall);

        var curPosition = _rigidbody.position;
        var movingDirection = _characterControlManager.MovingDirection;

        DOTween.Sequence()
            .Append(
                _rigidbody.DOMove(
                    curPosition +
                    movingDirection *
                    _data.FallControlData.FallDistance,
                    _data.FallControlData.FallPeriod))
            .AppendInterval(_data.FallControlData.FallIdlePeriod)
            .SetUpdate(UpdateType.Fixed)
            .OnComplete(() => ChangeState(State.Walking));
    }

    private void OnAttracted()
    {
        ChangeState(State.Stuck);

        DOTween.Sequence()
            .AppendInterval(_data.StuckControlData.StuckPeriod)
            .OnComplete(() => ChangeState(State.Walking));
    }

    private void ChangeState(State state)
    {
        _state = state;
        _spriteControlCore.ChangeSprite(state);
    }

    private void FixedUpdate()
    {
        if (_state != State.Walking)
            return;

        _rigidbody.MovePosition(
            _rigidbody.position +
            _characterControlManager.GetDeltaPosition(Time.deltaTime));
    }

    #region IPlayerSpriteControlTarget

    public Vector2 GetMovingDirection() => _characterControlManager.MovingDirection;

    #endregion
}
