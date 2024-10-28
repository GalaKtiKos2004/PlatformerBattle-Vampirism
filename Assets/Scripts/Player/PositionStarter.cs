using UnityEngine;

public class PositionStarter : MonoBehaviour
{
    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    public void StartGame()
    {
        transform.position = _startPosition;
    }
}
