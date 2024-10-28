using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColliderDetector))]
public abstract class Drummer : Fighter
{
    [SerializeField] private LayerMask _attackedLayer;
    [SerializeField] private Vector2 _colliderSize;

    [SerializeField] private float _damage;
    [SerializeField] private float _attackColldown = 3f;

    private WaitForSeconds _wait;

    private ColliderDetector _detector;
    private Attacker _attacker;

    private bool _canAttack;

    protected virtual void Awake()
    {
        _wait = new WaitForSeconds(_attackColldown);
        _attacker = new Attacker();
        _detector = GetComponent<ColliderDetector>();
        _canAttack = true;
    }

    protected void TryAttack()
    {
        if (_canAttack == false)
        {
            return;
        }

        if (_attacker.TryAttack(_damage, _detector, transform, _attackedLayer, _colliderSize))
        {
            StartCoroutine(AttackColldown());
        }
    }

    private IEnumerator AttackColldown()
    {
        _canAttack = false;
        yield return _wait;
        _canAttack = true;
    }
}