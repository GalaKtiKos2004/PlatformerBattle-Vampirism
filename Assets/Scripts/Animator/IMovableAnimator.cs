using UnityEngine;

[RequireComponent(typeof(IMovable))]
[RequireComponent(typeof(Animator))]
public class IMovableAnimator : MonoBehaviour
{
    private const string SpeedParametr = "Speed";

    private Animator _animator;
    private IMovable _mover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<IMovable>();
    }

    private void OnEnable()
    {
        _mover.Moved += SetSpeed;
    }

    private void OnDisable()
    {
        _mover.Moved -= SetSpeed;
    }

    private void SetSpeed(float speed)
    {
        _animator.SetFloat(SpeedParametr, Mathf.Abs(speed));
    }
}
