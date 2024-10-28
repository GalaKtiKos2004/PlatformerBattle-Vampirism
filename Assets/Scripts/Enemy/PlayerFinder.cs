using System;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;

    public event Action Collide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_playerLayer & (1 << collision.gameObject.layer)) != 0)
        {
            Collide?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_playerLayer & (1 << collision.gameObject.layer)) != 0)
        {
            Collide?.Invoke();
        }
    }
}