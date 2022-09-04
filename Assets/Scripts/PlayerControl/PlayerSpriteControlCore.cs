using System.Collections;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerSpriteControlCore
    {
        private readonly IPlayerSpriteControlTarget _controlTarget;
        private readonly Animator _animator;

        private Coroutine _walkingSpriteChangingCoroutine;

        private readonly int _idleState = Animator.StringToHash("Idle");
        private readonly int _walkingForward = Animator.StringToHash("WalkingForward");
        private readonly int _walkingBackward = Animator.StringToHash("WalkingBackward");
        private readonly int _walkingLeft = Animator.StringToHash("WalkingLeft");
        private readonly int _walkingRight = Animator.StringToHash("WalkingRight");
        private readonly int _stuckState = Animator.StringToHash("Stuck");
        private readonly int _fallState = Animator.StringToHash("Fall");
        private readonly int _drunkState = Animator.StringToHash("Drunk");

        public PlayerSpriteControlCore(
            IPlayerSpriteControlTarget target, Animator animator)
        {
            _controlTarget = target;
            _animator = animator;
        }

        public void ChangeSprite(Player.State state)
        {
            if (_walkingSpriteChangingCoroutine != null) {
                _controlTarget.StopCoroutine(_walkingSpriteChangingCoroutine);
                _walkingSpriteChangingCoroutine = null;
            }

            if (state != Player.State.Walking) {
                var targetAnimation = state switch {
                    Player.State.Idle => _idleState,
                    Player.State.Fall => _fallState,
                    Player.State.Drunk => _drunkState,
                    Player.State.Stuck => _stuckState
                };
                _animator.Play(targetAnimation);
            } else {
                _walkingSpriteChangingCoroutine =
                    _controlTarget.StartCoroutine(
                        WalkingSpriteChangingCoroutine());
            }
        }

        private IEnumerator WalkingSpriteChangingCoroutine()
        {
            while (true) {
                var movingDirection = _controlTarget.GetMovingDirection();
                var degree = Vector2.SignedAngle(Vector2.up, movingDirection);
                var animationState =
                    Mathf.Approximately(movingDirection.magnitude, 0)
                        ? _idleState
                        : degree switch {
                            > -45 and <= 45 => _walkingBackward,
                            > 45 and <= 135 => _walkingLeft,
                            > -135 and <= -45 => _walkingRight,
                            _ => _walkingForward
                        };

                _animator.Play(animationState);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public interface IPlayerSpriteControlTarget
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
        public void StopCoroutine(Coroutine coroutine);
        public Vector2 GetMovingDirection();
    }
}
