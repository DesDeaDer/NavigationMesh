using UnityEngine;
using UnityEngine.UI;

public class WinView : ViewBase {
    [SerializeField] private LevetTargetKillsCounter _levetTargetKillsCounter;
    [SerializeField] private Text _killedEnemyText;

    public LevetTargetKillsCounter LevetTargetKillsCounter => _levetTargetKillsCounter;
    public string KilledEnemy {
        set => _killedEnemyText.text = value;
    }

    public void Replay() {
        Hide();
        SceneID.Game.LoadScene();
    }

    public void Back() {
        Hide();
        SceneID.Menu.LoadScene();
    }

    void OnEnable()
        => KilledEnemy = string.Format("{0}/{1}", LevetTargetKillsCounter.KillsCurrent, LevetTargetKillsCounter.CountKills);
}
