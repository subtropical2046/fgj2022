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

        #region Public Properties

        public Vector2 MovingDirection { get; private set; }

        #endregion

        #region Private Members

        private SmoothMove _smoothMove;

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
            MovingDirection = context.ReadValue<Vector2>();
        }

        #endregion

        public Vector2 GetDeltaPosition(float deltaTime)
        {
            return _smoothMove.GetDeltaMovement(MovingDirection, deltaTime);
        }
    }
}
