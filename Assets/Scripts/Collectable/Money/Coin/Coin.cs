using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Destroy(gameObject);
    }
}
