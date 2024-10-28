using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(PlayerInput))]
public class PlayerMover : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private PlayerInput _input;

    private Quaternion _rightAngle = Quaternion.Euler(Vector3.zero);
    private Quaternion _leftAngle = Quaternion.Euler(0f, 180f, 0f);

    public event Action <float> Moved;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = new Vector2(_input.MoveInput * _speed, _rigidbody.velocity.y);

        Rotate(direction.x);
        Moved?.Invoke(direction.x);
        _rigidbody.velocity = direction;
    }

    private void Rotate(float direction)
    {
        if (direction > 0)
            transform.rotation = _rightAngle;
        else if (direction < 0)
            transform.rotation = _leftAngle;
    }
}
