using CharacterControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class LightController : MonoBehaviour
{
    [SerializeField] private CharacterControlManager _characterControlManager;
    [SerializeField] private float _decideRandomMoveInterval = default;
    [SerializeField] private int _randomMoveRate = default;
    [SerializeField] private float _randomMoveSpeed = default;

    private LightState _currentState;
    private bool _keepRandomMove;
    private enum LightState
    {
        InPlayerControl,
        RandomMove,
        Attracted
    }
    
    private void Start()
    {
        _currentState = LightState.InPlayerControl;
        _keepRandomMove = true;
        StartCoroutine(DecideRandomMove());
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)_characterControlManager.GetDeltaPosition(Time.deltaTime);
    }

    private IEnumerator DecideRandomMove()
    {
        while (_keepRandomMove)
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
            if (!(_currentState == LightState.Attracted))
            {
                //move light transform to target transform
                //PlayGetAttractionAnimation(collision.transform);
            }
        }
    }

    private Vector2 GetRandomMovePosition()
    {
        int x = Random.Range(-9, 10);
        int y = Random.Range(-5, 6);
        return new Vector2(x, y);
    }
}
