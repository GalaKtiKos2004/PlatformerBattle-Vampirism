using UnityEngine;

public class MedecineChest : MonoBehaviour, ICollectable
{
    [SerializeField] private float _recoverHealth = 5f;

    public float RecoverHealth => _recoverHealth;

    public void Collect()
    {
        Destroy(gameObject);
    }
}