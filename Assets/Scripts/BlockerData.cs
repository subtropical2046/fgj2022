using UnityEngine;

[CreateAssetMenu(menuName = "Data/Blocker Data")]
public class BlockerData : ScriptableObject
{
    [Header("SpriteSet")]
    [SerializeField] private Sprite _animationSprite1;
    [SerializeField] private Sprite _animationSprite2;
    [SerializeField] private Sprite _blockSprite;
    [Header("Adjustable Values")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _animationSpriteSwapSpeed;

    public Sprite AnimationSprite1 => _animationSprite1;
    public Sprite AnimationSprite2 => _animationSprite2;
    public Sprite BlockSprite => _blockSprite;
    public float MoveSpeed => _moveSpeed;
    public float AnimationSpriteSwapSpeed => _animationSpriteSwapSpeed;
}
