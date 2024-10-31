using System.Linq;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;

    public bool GetEnemyInsideCircle(CircleCollider2D circleCollider, out Fighter enemy)
    {
        Vector2 circleCenter = transform.position;
        float circleRadius = circleCollider.radius * transform.localScale.x;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(circleCenter, circleRadius);

        Collider2D[] Sortedcolliders = colliders.OrderBy(collider => Vector2.Distance(circleCenter, collider.transform.position)).ToArray();

        foreach (Collider2D collider in Sortedcolliders)
        {
            if (IsEnemy(collider, out Fighter fighter))
            {
                enemy = fighter;
                return true;
            }
        }

        enemy = null;
        return false;
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
}
