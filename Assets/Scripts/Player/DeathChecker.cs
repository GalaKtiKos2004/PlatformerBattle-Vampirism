using UnityEngine;

public class DeathChecker : MonoBehaviour
{
    [SerializeField] private PositionStarter _positionStarter;

    private Vector3 _startPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<DeathTrigger>(out _))
        {
            _positionStarter.StartGame();
        }
    }
}
