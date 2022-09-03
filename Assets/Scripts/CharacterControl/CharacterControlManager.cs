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

        #endregion

        #region Private Members

        private SmoothMove _smoothMove;
        private Vector2 _movingDirection;

        #endregion

        private void Awake()
        {
            _smoothMove = new SmoothMove(5, 0.2f);
        }

        #region Input System Event

        public void OnMove(InputAction.CallbackContext context)
        {
            _movingDirection = context.ReadValue<Vector2>();
        }

        #endregion

        private void FixedUpdate()
        {
            transform.localPosition +=
                (Vector3)_smoothMove.GetDeltaMovement(
                    _movingDirection, Time.deltaTime);
        }
    }
}
