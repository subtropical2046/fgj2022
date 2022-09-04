using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Blocker : MonoBehaviour
{
    [SerializeField] private BlockerData _blockerData;

    private SpriteRenderer _spriteRenderer;
    private bool _triggered = false;
    private Vector2 _playerPos;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_triggered && collision.CompareTag("Player"))
        {
            _playerPos = collision.gameObject.transform.position;
            _triggered = true;
            DoBlock();
        }
    }

    private void DoBlock()
    {
        // Play Move Animation
        StartCoroutine(PlayMoveAnimation());
        // Move GameObject
        transform.DOMove(_playerPos, _blockerData.MoveSpeed)
            .OnComplete(() => OnDoBlockComplete());
    }

    private IEnumerator PlayMoveAnimation()
    {
        _spriteRenderer.flipX = (transform.position.x <= _playerPos.x);
        while (true)
        {
            _spriteRenderer.sprite = _blockerData.AnimationSprite1;
            yield return new WaitForSeconds(_blockerData.AnimationSpriteSwapSpeed);
            _spriteRenderer.sprite = _blockerData.AnimationSprite2;
            yield return new WaitForSeconds(_blockerData.AnimationSpriteSwapSpeed);
        }
    }

    private void OnDoBlockComplete()
    {
        StopAllCoroutines();
        _spriteRenderer.sprite = _blockerData.BlockSprite;
        this.enabled = false;
    }
}
