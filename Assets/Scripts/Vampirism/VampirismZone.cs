using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(EnemyDetector))]
public class VampirismZone : MonoBehaviour
{
    [SerializeField] private Fighter _playerFighter;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _vampirismTime;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _damage;

    private CircleCollider2D _circleCollider;

    private EnemyDetector _enemyDetector;

    private bool _isVampir;
    private bool _isCooldown;

    private float _maxManaValue;
    private float _minManaValue;
    private float _currentManaValue;

    public event Action VampirismStarted;
    public event Action VampirismEnded;
    public event Action<float> ManaChanged;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _enemyDetector = GetComponent<EnemyDetector>();
        _isVampir = false;
        _maxManaValue = 1f;
        _minManaValue = 0f;
        _currentManaValue = _maxManaValue;
    }

    private void OnEnable()
    {
        _playerInput.Vampiring += TryStartVampirism;
    }

    private void OnDisable()
    {
        _playerInput.Vampiring -= TryStartVampirism;
    }

    private void TryStartVampirism()
    {
        if (_isCooldown == false && _isVampir == false)
        {
            StartCoroutine(ActivateVampirism(_minManaValue, _vampirismTime));
            VampirismStarted?.Invoke();
        }
    }

    private void ManaTransition(ref float elapsedTime, float previousValue, float duration, float target)
    {
        elapsedTime += Time.deltaTime;

        float normalizedPosition = elapsedTime / duration;
        _currentManaValue = Mathf.Lerp(previousValue, target, normalizedPosition);

        ManaChanged?.Invoke(_currentManaValue);
    }

    private IEnumerator ActivateVampirism(float target, float duration)
    {
        float recivedDamage;
        float elapsedTime = 0f;
        float previousValue = _currentManaValue;

        _isVampir = true;

        while (elapsedTime < duration)
        {
            ManaTransition(ref elapsedTime, previousValue, duration, target);

            if (_enemyDetector.GetEnemyInsideCircle(_circleCollider, out Fighter fighter) && target == _minManaValue)
            {
                recivedDamage = fighter.TakeDamage(_damage * Time.deltaTime);
                _playerFighter.TryAddHealth(recivedDamage);
            }

            yield return null;
        }

        _isVampir = false;

        VampirismEnded?.Invoke();

        _isCooldown = true;
        StartCoroutine(Colldown(_maxManaValue, _cooldownTime));
    }

    private IEnumerator Colldown(float target, float duration)
    {
        float elapsedTime = 0f;
        float previousValue = _currentManaValue;

        _isCooldown = true;

        while (elapsedTime < duration)
        {
            ManaTransition(ref elapsedTime, previousValue, duration, target);

            yield return null;
        }

        _isCooldown = false;
    }
}
