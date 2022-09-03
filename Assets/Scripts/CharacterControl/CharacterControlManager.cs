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

        private Vector2 _movingDirection;

        #endregion

        #region Input System Event

        public void OnMove(InputAction.CallbackContext context)
        {
            _movingDirection = context.ReadValue<Vector2>();
        }

        #endregion
    }
}
