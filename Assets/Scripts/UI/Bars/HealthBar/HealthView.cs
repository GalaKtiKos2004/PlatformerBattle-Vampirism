using UnityEngine;

public class HealthView : BarView
{
    [SerializeField] private float _smooothDecreaseDuration = 0.25f;

    private Health _health;

    private void OnDisable()
    {
        _health.Changed -= UpdateBar;
    }

    public void Init(Health health)
    {
        _health = health;

        _health.Changed += UpdateBar;
    }

    private void UpdateBar(float value, float maxValue)
    {
        StartCoroutine(DecreaseBarSmoothly(value / maxValue, _smooothDecreaseDuration));
    }
}
