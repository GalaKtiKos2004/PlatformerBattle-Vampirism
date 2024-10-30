using System.Collections;
using UnityEngine;

public class VampirismView : BarView
{
    [SerializeField] private VampirismZone _vampirismZone;

    private float _minBarValue = 0f;
    private float _maxBarValue = 1f;
    private float _vampirismTime;
    private float _cooldownTime;

    protected override void Awake()
    {
        base.Awake();
        _vampirismTime = _vampirismZone.VampirismTime;
        _cooldownTime = _vampirismZone.CooldownTime;
    }

    private void OnEnable()
    {
        _vampirismZone.VampirismStarted += UpdateBar;
    }

    private void OnDisable()
    {
        _vampirismZone.VampirismStarted -= UpdateBar;
    }

    protected void UpdateBar()
    {
        StartCoroutine(DecreaseBarSmoothly(_minBarValue, _vampirismTime));
    }

    protected override IEnumerator DecreaseBarSmoothly(float targetValue, float smooothDecreaseDuration)
    {
        yield return base.DecreaseBarSmoothly(targetValue, smooothDecreaseDuration);

        if (targetValue == _minBarValue)
        {
            StartCoroutine(DecreaseBarSmoothly(_maxBarValue, _cooldownTime));
        }
    }
}
