using UnityEngine;
using UnityEngine.UI;

public class WinView : ViewBase
{
    [SerializeField] private LevetTargetKillsCounter _levetTargetKillsCounter;
    [SerializeField] private Text _killedEnemyText;

    public LevetTargetKillsCounter LevetTargetKillsCounter
    {
        get
        {
            return _levetTargetKillsCounter;
        }
    }

    public string KilledEnemy
    {
        set
        {
            _killedEnemyText.text = value;
        }
    }

    //Call from UI
    public void Replay()
    {
        Hide();
        SceneID.Game.LoadScene();
    }

    //Call from UI
    public void Back()
    {
        Hide();
        SceneID.Menu.LoadScene();
    }

    private void OnEnable()
    {
        KilledEnemy = string.Format("{0}/{1}", LevetTargetKillsCounter.KillsCurrent, LevetTargetKillsCounter.CountKills);
    }
}
