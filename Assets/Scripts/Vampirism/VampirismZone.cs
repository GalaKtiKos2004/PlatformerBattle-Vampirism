using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class VampirismZone : MonoBehaviour
{
    [SerializeField] private Color _vampirColor;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Fighter _playerFighter;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _vampirismTime;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _damage;

    private SpriteRenderer _spriteRenderer;

    private CircleCollider2D _circleCollider;

    private WaitForSeconds _cooldown;

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

    private List<Fighter> GetPlayersInsideCircle()
    {
        Vector2 circleCenter = transform.position;
        float circleRadius = _circleCollider.radius * transform.localScale.x;

        List<Fighter> fighters = new List<Fighter>();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(circleCenter, circleRadius);

        Debug.Log(colliders.Length);

        foreach (Collider2D collider in colliders)
        {
            if (IsEnemy(collider, out Fighter fighter))
            {
                fighters.Add(fighter);
            }
        }

        return fighters;
    }

    private bool IsEnemy(Collider2D collision, out Fighter fighter)
    {
        fighter = null;

        if ((_enemyLayer.value & (1 << collision.gameObject.layer)) == 0)
        {
            return false;
        }

        if (collision.TryGetComponent(out fighter) == false)
        {
            return false;
        }

        return true;
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

        _spriteRenderer.color = _vampirColor;
        _isVampir = true;

        while (elapsedTime < _vampirismTime)
        {
            elapsedTime += Time.deltaTime;

            foreach (Fighter fighter in GetPlayersInsideCircle())
            {
                fighter.TakeDamage(_damage * Time.deltaTime);
                _playerFighter.TryAddHealth(_damage * Time.deltaTime);
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
