using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string Jump = "Jump";

    private const KeyCode AttackKey = KeyCode.Z;

    public float MoveInput {  get; private set; }

    public event Action Jumped;
    public event Action Attacked;

    private void Awake()
    {
        MoveInput = 0;
    }

    private void Update()
    {
        MoveInput = Input.GetAxisRaw(Horizontal);

        if (Input.GetButtonDown(Jump) || Input.GetButtonDown(Vertical))
        {
            Jumped?.Invoke();
        }

        if (Input.GetKeyDown(AttackKey))
        {
            Attacked?.Invoke();
        }
    }
}
