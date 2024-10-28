using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _coinsPoint;
    [SerializeField] private Coin _coinPrefab;

    private void Start()
    {
        foreach (Transform coin in _coinsPoint)
        {
            Instantiate(_coinPrefab, coin.position, coin.rotation);
        }
    }
}
