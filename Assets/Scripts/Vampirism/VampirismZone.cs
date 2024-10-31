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

    private WaitForSeconds _cooldown;

    private EnemyDetector _enemyDetector;

    private Color _normalColor;

    private bool _isVampir;
    private bool _isCooldown;

    private float _maxManaValue;
    private float _minManaValue;
    private float _currentManaValue;

    public event Action VampirismStarted;
    public event Action VampirismEnded;
    public event Action<float> ManaChanged;

    public float VampirismTime => _vampirismTime;
    public float CooldownTime => _cooldownTime;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _enemyDetector = GetComponent<EnemyDetector>();
        _cooldown = new WaitForSeconds(_cooldownTime);
        _isVampir = false;
        _maxManaValue = 1f;
        _minManaValue = 0f;
        _currentManaValue = _maxManaValue;
    }

    private void OnEnable()
    {
        _playerInput.Vampiring += StartVampirism;
    }

    private void OnDisable()
    {
        _playerInput.Vampiring -= StartVampirism;
    }

    private void StartVampirism()
    {
        if (_isCooldown == false && _isVampir == false)
        {
            StartCoroutine(ToogleVampirism(_minManaValue, _vampirismTime));
            VampirismStarted?.Invoke();
        }
    }

    private IEnumerator ToogleVampirism(float target, float duration)
    {
        float recivedDamage;
        float elapsedTime = 0f;
        float previousValue = _currentManaValue;

        _isVampir = true;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / duration;
            _currentManaValue = Mathf.Lerp(previousValue, target, normalizedPosition);

            ManaChanged?.Invoke(_currentManaValue);

            if (_enemyDetector.GetEnemyInsideCircle(_circleCollider, out Fighter fighter))
            {
                recivedDamage = fighter.TakeDamage(_damage * Time.deltaTime);
                _playerFighter.TryAddHealth(recivedDamage);
            }

            yield return null;
        }

        _isVampir = false;

        VampirismEnded?.Invoke();

        _isCooldown = false;

        if (target == _minManaValue)
        {
            _isCooldown = true;
            StartCoroutine(ToogleVampirism(_maxManaValue, _cooldownTime));
        }
    }

    private IEnumerator VampirismCooldown()
    {
        _isCooldown = true;

        yield return _cooldown;

        _isCooldown = false;
    }
}
