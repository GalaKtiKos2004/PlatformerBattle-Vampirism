using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(EnemyDetector))]
public class VampirismZone : MonoBehaviour
{
    [SerializeField] private Color _vampirColor;
    [SerializeField] private Fighter _playerFighter;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _vampirismTime;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _damage;

    private SpriteRenderer _spriteRenderer;

    private CircleCollider2D _circleCollider;

    private WaitForSeconds _cooldown;

    private EnemyDetector _enemyDetector;

    private Color _normalColor;

    private bool _isVampir;
    private bool _isCooldown;

    public event Action VampirismStarted;

    public float VampirismTime => _vampirismTime;
    public float CooldownTime => _cooldownTime;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _enemyDetector = GetComponent<EnemyDetector>();
        _normalColor = _spriteRenderer.color;
        _cooldown = new WaitForSeconds(_cooldownTime);
        _isVampir = false;
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
            StartCoroutine(ActivateVampirismEffect());
            VampirismStarted?.Invoke();
        }
    }

    private IEnumerator ActivateVampirismEffect()
    {
        float elapsedTime = 0f;
        float recivedDamage;

        _spriteRenderer.color = _vampirColor;
        _isVampir = true;

        while (elapsedTime < _vampirismTime)
        {
            elapsedTime += Time.deltaTime;

            if (_enemyDetector.GetEnemyInsideCircle(_circleCollider, out Fighter fighter))
            {
                recivedDamage = fighter.TakeDamage(_damage * Time.deltaTime);
                _playerFighter.TryAddHealth(recivedDamage);
            }

            yield return null;
        }

        _spriteRenderer.color = _normalColor;
        _isVampir = false;

        StartCoroutine(VampirismCooldown());
    }

    private IEnumerator VampirismCooldown()
    {
        _isCooldown = true;

        yield return _cooldown;

        _isCooldown = false;
    }
}
