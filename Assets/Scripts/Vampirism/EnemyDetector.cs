using System.Linq;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;

    public bool GetEnemyInsideCircle(CircleCollider2D circleCollider, out Fighter enemy)
    {
        Vector2 circleCenter = transform.position;
        float circleRadius = circleCollider.radius * transform.localScale.x;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(circleCenter, circleRadius, _enemyLayer);

        Collider2D[] Sortedcolliders = colliders.OrderBy(collider => 
                                        Vector2.SqrMagnitude(circleCenter - (Vector2)collider.transform.position)).ToArray();

        foreach (Collider2D collider in Sortedcolliders)
        {
            if (collider.TryGetComponent(out Fighter fighter))
            {
                enemy = fighter;
                return true;
            }
        }

        enemy = null;
        return false;
    }
}
