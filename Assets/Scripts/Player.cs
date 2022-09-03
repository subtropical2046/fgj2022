using CharacterControl;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]
    private CharacterControlManager _characterControlManager;
    [SerializeField]
    private Rigidbody2D _rigidbody;

    #endregion

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(
            _rigidbody.position +
            _characterControlManager.GetDeltaPosition(Time.deltaTime));
    }
}
