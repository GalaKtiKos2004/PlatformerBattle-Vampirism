using UnityEngine;

public class ColliderDetector : MonoBehaviour
{
    public bool IsGrounded(Transform checkPoint, LayerMask layerMask, Vector2 boxSize, out Collider2D hitCollider)
    {
        hitCollider = Physics2D.OverlapBox(checkPoint.position, boxSize, checkPoint.eulerAngles.z, layerMask);

        return hitCollider != null;
    }
}
