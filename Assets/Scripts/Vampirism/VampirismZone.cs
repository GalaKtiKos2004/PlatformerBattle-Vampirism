using System.Collections;
using UnityEngine;

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

    private WaitForSeconds _cooldown;

    private Color _normalColor;

    private Coroutine _vampirismCorutine;

    private bool _isVampir;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanUseVampirism(collision, out Fighter fighter) == false)
        {
            return;
        }

        StartCoroutine(Vampirism());

        fighter.TakeDamage(_damage * Time.deltaTime);
        _playerFighter.TryAddHealth(_damage * Time.deltaTime);
    }

    private bool CanUseVampirism(Collider2D collision, out Fighter fighter)
    {
        fighter = null;

        if ((_enemyLayer.value & (1 << collision.gameObject.layer)) == 0)
        {
            return false;
        }

        if (!_isVampir)
        {
            return false;
        }

        if (!collision.TryGetComponent(out fighter))
        {
            return false;
        }

        return true;
    }

    private void StartVampirism()
    {
        StartCoroutine(ActivateVampirismEffect());
    }

    private IEnumerator ActivateVampirismEffect()
    {
        float elapsedTime = 0f;

        _spriteRenderer.color = _vampirColor;
        _isVampir = true;

        while (elapsedTime < _vampirismTime)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _spriteRenderer.color = _normalColor;
    }

    private IEnumerator Vampirism()
    {
        while (enabled)
        {
            yield return null;
        }
    }
}
