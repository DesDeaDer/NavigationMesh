using System;
using UnityEngine;

public class EnemyHealthViewController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ViewsController _viewsController;

    public PlayerController PlayerController
    {
        get
        {
            return _playerController;
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
        PlayerController.OnChangeState += OnChangeStateHandler;
    }

    private void OnDisable()
    {
        if (PlayerController)
        {
            PlayerController.OnChangeState -= OnChangeStateHandler;
        }
    }

    private void OnChangeStateHandler()
    {
        var view = ViewsController.Get<EnemyHealthView>();

        switch (PlayerController.StateCurrent)
        {
            case PlayerController.State.Figth:
                view.HealthBehaviour = PlayerController.AttackBehavior.Target;
                view.Show();
                break;
            default:
                view.Hide();
                break;
        }
    }

}
