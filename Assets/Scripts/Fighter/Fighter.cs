using System;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    private Health _health;
    
    protected Health Health => _health;

    public event Action HealthCreating;


    private void OnDisable()
    {
        _health.Died -= Die;
    }

    public void Init(Health health)
    {
        _health = health;
        _health.Died += Die;
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    protected void CreateNewHealth()
    {
        HealthCreating?.Invoke();
    }

    public bool TryAddHealth(float recoverHealth) => _health.TryTreated(recoverHealth);

    protected abstract void Die();
}