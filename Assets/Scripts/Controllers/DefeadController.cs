using UnityEngine;

public class DefeadController : MonoBehaviour
{

    [SerializeField] private HealthBehaviour _healthBehaviour;
    [SerializeField] private ViewsController _viewsController;

    public HealthBehaviour HealthBehaviour => _healthBehaviour;
    public ViewsController ViewsController => _viewsController;

    void OnEnable() 
        => HealthBehaviour.OnDie += OnDieHandler;

    void OnDisable()
        => HealthBehaviour?.OnDie -= OnDieHandler;

    void OnDieHandler() 
        => ViewsController.Show<DefeadView>();
}
