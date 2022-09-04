using CharacterControl;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] private CharacterControlManager _characterControlManager;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] LightControllerData lightControllerData;

    private Light2D _light;
    private float _defaultLightIntensity;
    private Sequence _randomMoveSequence;
    private bool _turnOnOffRandomMove;

    public void OnGameStart()
    {
        StartCoroutine(DecideRandomMove());
    }

    private void Start()
    {
        _light = GetComponent<Light2D>();
        _defaultLightIntensity = _light.intensity;
        _turnOnOffRandomMove = lightControllerData.TunrOnOffRandomMove;
        GameManager.Instance.OnGameStageChanged += StageChanged;
    }

    private void StageChanged(GameStage stage)
    {
        if(stage == GameStage.Play)
        {
            OnGameStart();
        }
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)_characterControlManager.GetDeltaPosition(Time.deltaTime);
    }

    private IEnumerator DecideRandomMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(lightControllerData.DecideRandomMoveInterval);
            if (RandomRateMeet() && _turnOnOffRandomMove)
            {
                FlashAndMoveToRandomPosition();
            }
        }

        bool RandomRateMeet()
        {
            return Random.Range(1, 101) <= lightControllerData.RandomMoveRate;
        }
    }

    private void FlashAndMoveToRandomPosition()
    {
        SoundManager.Instance.Play(Sound.LightMove);
        _randomMoveSequence = DOTween.Sequence();
        Tweener flash = DOTween.To(() => _light.intensity, x => _light.intensity = x, 0.2f, 0.2f).SetLoops(4, LoopType.Yoyo);
        Tweener move = transform.DOMove(GetRandomMovePosition(), lightControllerData.RandomMoveSpeed);
        _randomMoveSequence.Append(flash);
        _randomMoveSequence.Append(move);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //暫定tag為Attractor
        if (collision.CompareTag("Attractor"))
        {
            _randomMoveSequence.Kill();
            _turnOnOffRandomMove = false;
            _light.intensity = _defaultLightIntensity;
            transform.DOMove(collision.transform.position, lightControllerData.AttractedMoveSpeed)
                .OnComplete(() => _turnOnOffRandomMove = true);
        }
    }

    private Vector2 GetRandomMovePosition()
    {
        int x = Random.Range(-9, 10);
        int y = Random.Range(-5, 6);
        Vector2 returnPos = (Vector2)_playerTransform.position + new Vector2(x, y);
        return returnPos;
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnGameStageChanged -= StageChanged;
    }
}