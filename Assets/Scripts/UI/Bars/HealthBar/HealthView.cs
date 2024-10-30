using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthView : MonoBehaviour
{
    [SerializeField] private float _smooothDecreaseDuration = 0.25f;

    private Image _image;

    private Health _health;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

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
        StartCoroutine(DecreaseHealthSmoothly(value / maxValue));
    }

    private IEnumerator DecreaseHealthSmoothly(float targetHealth)
    {
        float elapsedTime = 0f;
        float previousValue = _image.fillAmount;

        while (elapsedTime < _smooothDecreaseDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / _smooothDecreaseDuration;
            float intermediateValue = Mathf.Lerp(previousValue, targetHealth, normalizedPosition);
            _image.fillAmount = intermediateValue;

            yield return null;
        }
    }
}
