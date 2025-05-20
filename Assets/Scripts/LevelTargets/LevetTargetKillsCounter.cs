using UnityEngine;

public class LevetTargetKillsCounter : MonoBehaviour, ITargetLevel {
    [SerializeField] private int _countKills;
    [SerializeField] private UserInfo _userInfo;

    public int CountKills => _countKills;
    public UserInfo UserInfo => _userInfo;
    
    public bool IsCompleted 
        => KillsCurrent >= CountKills;

    public int KillsCurrent { get; private set; }

    void OnEnable()
        => UserInfo.OnChangeKillsCount += OnChangeKillsCountHandler;

    void OnDisable()
        => UserInfo?.OnChangeKillsCount -= OnChangeKillsCountHandler;

    void OnChangeKillsCountHandler()
        => ++KillsCurrent;
}
