using UnityEngine;

namespace CharacterControl
{
    [CreateAssetMenu(
        menuName = "Data/Character Control Data")]
    public class CharacterControlData : ScriptableObject
    {
        [SerializeField]
        private float _movingVelocity;
        [SerializeField]
        private float _movingAccelTime;

        public float MovingVelocity => _movingVelocity;
        public float MovingAccelTime => _movingAccelTime;
    }
}
