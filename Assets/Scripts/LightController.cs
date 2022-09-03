using CharacterControl;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class LightController : MonoBehaviour
{
    [SerializeField] private CharacterControlManager _characterControlManager;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] LightControllerData lightControllerData;


    private void Start()
    {
        lightControllerData._tunrOnOffRandomMove = true;
        StartCoroutine(DecideRandomMove());
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)_characterControlManager.GetDeltaPosition(Time.deltaTime);
    }

    private IEnumerator DecideRandomMove()
    {
        while (lightControllerData._tunrOnOffRandomMove)
        {
            yield return new WaitForSeconds(lightControllerData._decideRandomMoveInterval);
            if (Random.Range(1, 101) <= lightControllerData._randomMoveRate)
            {
                transform.DOMove(GetRandomMovePosition(), lightControllerData._randomMoveSpeed);
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
            transform.DOMove(collision.transform.position, lightControllerData._attractedMoveSpeed);
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
