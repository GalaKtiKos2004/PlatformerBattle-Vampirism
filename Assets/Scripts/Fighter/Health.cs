using System;

public class Health
{
    public event Action Died;
    public event Action<float, float> Changed;

    public Health(float maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public float MaxHealth { get; private set; }

    public float CurrentHealth { get; private set; }

    public float TakeDamage(float damage)
    {
        float recivedDamage;

        if (CurrentHealth < damage)
        {
            recivedDamage = CurrentHealth;
            Died?.Invoke();
        }
        else
        {
            CurrentHealth -= damage;
            recivedDamage = damage;
        }

        Changed?.Invoke(CurrentHealth, MaxHealth);

        return recivedDamage;
    }

    public bool TryTreated(float recoverHealth)
    {
        if (CurrentHealth + recoverHealth > MaxHealth)
        {
            return false;
        }
        else
        {
            CurrentHealth += recoverHealth;
            Changed?.Invoke(CurrentHealth, MaxHealth);
            return true;
        }
    }
}
