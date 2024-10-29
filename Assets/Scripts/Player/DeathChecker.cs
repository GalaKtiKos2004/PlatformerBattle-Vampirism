using UnityEngine;

public class DeathChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private PositionStarter _positionStarter;

    private Vector3 _startPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_playerLayer.value & (1 << gameObject.gameObject.layer)) == 0)
        {
            return;
        }

        if (collision.TryGetComponent<DeathTrigger>(out _))
        {
            _positionStarter.StartGame();
        }
    }
}
