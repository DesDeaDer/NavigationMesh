using UnityEngine;

public class EndLevel : MonoBehaviour {
    [SerializeField] private Transform _player;
    [SerializeField] private LevelTargets _levelTargets;
    [SerializeField] private ViewsController _viewsController;

    public Transform Player => _player;
    public LevelTargets LevelTargets => _levelTargets;
    public ViewsController ViewsController => _viewsController;
    void OnTriggerEnter(Collider other) {
        if (other.transform == Player)
            if (LevelTargets.IsCompleted)
                ViewsController.Show<WinView>();
            else {
                var killsCounter = LevelTargets.Get<LevetTargetKillsCounter>();
                var view = ViewsController.Get<InfoView>();
                var killsRemained = killsCounter.CountKills - killsCounter.KillsCurrent;
                view.Text = $"Need to kill {killsRemained} more enemies";
                view.Show();
            }
    }
}
