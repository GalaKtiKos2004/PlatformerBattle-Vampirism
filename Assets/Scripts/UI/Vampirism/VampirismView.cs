using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class VampirismView : MonoBehaviour
{
    Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
}
