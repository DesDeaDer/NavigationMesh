using UnityEngine;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private LevelTargets _levelTargets;
    [SerializeField] private ViewsController _viewsController;

    public Transform Player
    {
        get
        {
            return _player;
        }
    }

    public LevelTargets LevelTargets
    {
        get
        {
            return _levelTargets;
        }
    }

    public ViewsController ViewsController
    {
        get
        {
            return _viewsController;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == Player)
        {
            if (LevelTargets.IsCompleted)
            {
                ViewsController.Show<WinView>();
            }
            else
            {
                var killsCounter = LevelTargets.Get<LevetTargetKillsCounter>();
                var view = ViewsController.Get<InfoView>();
                view.Text = string.Format("Need to kill {0} more enemies", killsCounter.CountKills - killsCounter.KillsCurrent);
                view.Show();
            }
        }
    }
}
