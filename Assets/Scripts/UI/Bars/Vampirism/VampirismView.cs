using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class VampirismView : MonoBehaviour
{
    [SerializeField] private VampirismZone _vampirismZone;

    private Image _image;

    private float _minBarValue = 0f;
    private float _maxBarValue = 1f;
    private float _vampirismTime;
    private float _cooldownTime;

    private void Awake()
    {
        _image = GetComponent<Image>();
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

    private void UpdateBar()
    {
        StartCoroutine(DecreaseBarSmoothly(_minBarValue, _vampirismTime));
    }

    private IEnumerator DecreaseBarSmoothly(float targetValue, float smooothDecreaseDuration)
    {
        float elapsedTime = 0f;
        float previousValue = _image.fillAmount;

        while (elapsedTime < smooothDecreaseDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / smooothDecreaseDuration;
            float intermediateValue = Mathf.Lerp(previousValue, targetValue, normalizedPosition);
            _image.fillAmount = intermediateValue;

            yield return null;
        }

        if (targetValue == _minBarValue)
        {
            StartCoroutine(DecreaseBarSmoothly(_maxBarValue, _cooldownTime));
        }
    }
}
