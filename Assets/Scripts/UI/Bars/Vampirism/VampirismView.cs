using System.Collections;
using UnityEngine;

public class VampirismView : BarView
{
    [SerializeField] private VampirismZone _vampirismZone;

    private void OnEnable()
    {
        _vampirismZone.ManaChanged += ChangeBarValue;
    }

    private void OnDisable()
    {
        _vampirismZone.ManaChanged -= ChangeBarValue;
    }
}
