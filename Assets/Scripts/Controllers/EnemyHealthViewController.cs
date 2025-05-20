using System;
using UnityEngine;

public class EnemyHealthViewController : MonoBehaviour {
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ViewsController _viewsController;

    public PlayerController PlayerController => _playerController;
    public ViewsController ViewsController =>_viewsController;

    void OnEnable()
        => PlayerController.OnChangeState += OnChangeStateHandler;

    void OnDisable()
        => PlayerController?.OnChangeState -= OnChangeStateHandler;

    void OnChangeStateHandler() {
        var view = ViewsController.Get<EnemyHealthView>();

        if (PlayerController.StateCurrent == PlayerController.State.Figth) {
            view.HealthBehaviour = PlayerController.AttackBehavior.Target;
            view.Show();
        } else
            view.Hide();
    }
}
