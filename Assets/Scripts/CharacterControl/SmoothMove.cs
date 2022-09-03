using UnityEngine;

namespace CharacterControl
{
    /// <summary>
    /// The movement calculator
    /// </summary>
    public class SmoothMove
    {
        private readonly float _movingVelocity;
        private readonly float _movingAccelTime;

        private Vector2 _movingChangingVector;
        private Vector2 _curVelocity;

        public SmoothMove(float movingVelocity, float movingAccelTime)
        {
            _movingVelocity = movingVelocity;
            _movingAccelTime = movingAccelTime;
        }

        public Vector2 GetDeltaMovement(
            Vector2 targetDirection, float deltaTime)
        {
            _movingChangingVector = Vector2.SmoothDamp(
                _movingChangingVector, targetDirection,
                ref _curVelocity, _movingAccelTime, Mathf.Infinity,
                deltaTime);

            return _movingVelocity * deltaTime * _movingChangingVector;
        }
    }
}
