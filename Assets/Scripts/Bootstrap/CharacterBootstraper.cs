using UnityEngine;

[RequireComponent(typeof(Fighter))]
public class CharacterBootstraper : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;

    [SerializeField] private float _maxHealth = 100f;

    private Fighter _fighter;
    private Health _health;

    private void Awake()
    {
        _fighter = GetComponent<Fighter>();
        CreateNewHealth();
    }

    private void OnEnable()
    {
        _fighter.HealthCreating += CreateNewHealth;
    }

    private void OnDisable()
    {
        _fighter.HealthCreating -= CreateNewHealth;
    }

    private void CreateNewHealth()
    {
        _health = new Health(_maxHealth);
        _fighter.Init(_health);
        _healthView.Init(_health);
    }
}