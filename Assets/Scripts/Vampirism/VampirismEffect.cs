using UnityEngine;

[RequireComponent(typeof(VampirismZone))]
[RequireComponent(typeof(SpriteRenderer))]
public class VampirismEffect : MonoBehaviour
{
    [SerializeField] private Color _vampirColor;

    private VampirismZone _vampirismZone;
    private SpriteRenderer _spriteRenderer;

    private Color _normalColor;

    private void Awake()
    {
        _vampirismZone = GetComponent<VampirismZone>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _normalColor = _spriteRenderer.color;
    }

    private void OnEnable()
    {
        _vampirismZone.VampirismStarted += TurnOnEffect;
        _vampirismZone.VampirismEnded += TurnOffEffect;
    }

    private void OnDisable()
    {
        _vampirismZone.VampirismStarted -= TurnOnEffect;
        _vampirismZone.VampirismEnded -= TurnOffEffect;
    }

    private void TurnOnEffect()
    {
        _spriteRenderer.color = _vampirColor;
    }

    private void TurnOffEffect()
    {
        _spriteRenderer.color = _normalColor;
    }
}
