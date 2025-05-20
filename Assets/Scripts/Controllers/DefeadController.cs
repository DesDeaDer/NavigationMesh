using UnityEngine;

public class DefeadController : MonoBehaviour
{

    [SerializeField] private HealthBehaviour _healthBehaviour;
    [SerializeField] private ViewsController _viewsController;

    public HealthBehaviour HealthBehaviour => _healthBehaviour;
    public ViewsController ViewsController => _viewsController;

    private void OnEnable() 
        => HealthBehaviour.OnDie += OnDieHandler;

    private void OnDisable()
        => HealthBehaviour?.OnDie -= OnDieHandler;

    private void OnDieHandler() 
        => ViewsController.Show<DefeadView>();
}
