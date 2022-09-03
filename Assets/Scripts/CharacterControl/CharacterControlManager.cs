using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterControl
{
    /// <summary>
    /// The manager for controlling the character
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class CharacterControlManager : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField]
        private CharacterControlData _controlData;

        #endregion

        #region Private Members

        private SmoothMove _smoothMove;
        private Vector2 _movingDirection;

        #endregion

        private void Awake()
        {
            _smoothMove = new SmoothMove(
                _controlData.MovingVelocity, 
                _controlData.MovingAccelTime);
        }

        #region Input System Event

        public void OnMove(InputAction.CallbackContext context)
        {
            _movingDirection = context.ReadValue<Vector2>();
        }

        #endregion

        public Vector2 GetDeltaPosition(float deltaTime)
        {
            return _smoothMove.GetDeltaMovement(_movingDirection, deltaTime);
        }
    }
}
