using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Blocker : MonoBehaviour
{
    [Header("SpriteSet")]
    [SerializeField] private Sprite _animationSprite1;
    [SerializeField] private Sprite _animationSprite2;
    [SerializeField] private Sprite _blockSprite;
    [Header("BlockDestination")]
    [SerializeField] private Transform _destination;
    [Header("Adjustable Values")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _animationSpriteSwapSpeed;

    private SpriteRenderer _spriteRenderer;
    private bool _triggered = false;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.flipX = (transform.position.x >= _destination.position.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_triggered && collision.CompareTag("Player"))
        {
            _triggered = true;
            DoBlock();
        }
    }

    private void DoBlock()
    {
        // Play Move Animation
        StartCoroutine(PlayMoveAnimation());
        // Move GameObject
        transform.DOMove(_destination.position, _moveSpeed)
            .OnComplete(() => OnDoBlockComplete());
    }

    private IEnumerator PlayMoveAnimation()
    {
        while (true)
        {
            _spriteRenderer.sprite = _animationSprite1;
            yield return new WaitForSeconds(_animationSpriteSwapSpeed);
            _spriteRenderer.sprite = _animationSprite2;
        }
    }

    private void OnDoBlockComplete()
    {
        StopAllCoroutines();
        _spriteRenderer.sprite = _blockSprite;
        this.enabled = false;
    }
}
