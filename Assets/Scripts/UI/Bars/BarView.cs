using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class BarView : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    protected void ChangeBarValue(float value)
    {
        _image.fillAmount = value;
    }

    protected virtual IEnumerator DecreaseBarSmoothly(float targetValue, float smooothDecreaseDuration)
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
    }
}
