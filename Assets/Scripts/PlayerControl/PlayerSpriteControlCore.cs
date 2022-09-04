using System.Collections;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerSpriteControlCore
    {
        private readonly IPlayerSpriteControlTarget _controlTarget;
        private readonly Animator _animator;

        private Coroutine _walkingSpriteChangingCoroutine;

        private int _idleState = Animator.StringToHash("Idle");
        private int _walkingState = Animator.StringToHash("Walking");
        private int _stuckState = Animator.StringToHash("Stuck");
        private int _fallState = Animator.StringToHash("Fall");
        private int _drunkState = Animator.StringToHash("Drunk");

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
                Debug.Log(targetAnimation);
                _animator.Play(targetAnimation);
            }
        }

        private IEnumerator WalkingSpriteChangingCoroutine(Player.State state)
        {
            while (true) {
            }
        }
    }

    public interface IPlayerSpriteControlTarget
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
        public void StopCoroutine(Coroutine coroutine);
        public Vector2 GetVelocity();
    }
}
