using UnityEngine;

public class DefeadController : MonoBehaviour
{

    [SerializeField] private HealthBehaviour _healthBehaviour;
    [SerializeField] private ViewsController _viewsController;

    public HealthBehaviour HealthBehaviour
    {
        get
        {
            return _healthBehaviour;
        }
    }

    public ViewsController ViewsController
    {
        get
        {
            return _viewsController;
        }
    }

    private void OnEnable()
    {
        HealthBehaviour.OnDie += OnDieHandler;
    }

    private void OnDisable()
    {
        if (HealthBehaviour)
        {
            HealthBehaviour.OnDie -= OnDieHandler;
        }
    }

    private void OnDieHandler()
    {
        ViewsController.Show<DefeadView>();
    }
}
