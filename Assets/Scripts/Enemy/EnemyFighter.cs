using UnityEngine;

public class EnemyFighter : Drummer
{
    private void Update()
    {
        TryAttack();
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
