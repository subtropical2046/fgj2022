using CharacterControl;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class LightController : MonoBehaviour
{
    [SerializeField] private CharacterControlManager _characterControlManager;
    [SerializeField] private Transform _playerTransform;

    [Header("Adjustable Value")]
    [SerializeField] private bool _TunrOnOffRandomMove;
    [SerializeField] private float _decideRandomMoveInterval = default;
    [SerializeField] private int _randomMoveRate = default;
    [SerializeField] private float _randomMoveSpeed = default;
    [SerializeField] private float _attractedMoveSpeed = default;
    
    private void Start()
    {
        _TunrOnOffRandomMove = true;
        StartCoroutine(DecideRandomMove());
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)_characterControlManager.GetDeltaPosition(Time.deltaTime);
    }

    private IEnumerator DecideRandomMove()
    {
        while (_TunrOnOffRandomMove)
        {
            yield return new WaitForSeconds(_decideRandomMoveInterval);
            if (Random.Range(1, 101) <= _randomMoveRate)
            {
                transform.DOMove(GetRandomMovePosition(), _randomMoveSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //暫定tag為Attractor
        if (collision.CompareTag("Attractor"))
        {
            Debug.Log("OnTriggerEnter2D");
            DOTween.KillAll();
            transform.DOMove(collision.transform.position, _attractedMoveSpeed);
        }
    }

    private Vector2 GetRandomMovePosition()
    {
        int x = Random.Range(-9, 10);
        int y = Random.Range(-5, 6);
        Vector2 returnPos = (Vector2)_playerTransform.position + new Vector2(x, y);
        return returnPos;
    }
}
