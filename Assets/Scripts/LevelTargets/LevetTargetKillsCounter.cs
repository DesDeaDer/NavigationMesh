using UnityEngine;

public class LevetTargetKillsCounter : MonoBehaviour, ITargetLevel
{
    [SerializeField] private int _countKills;
    [SerializeField] private UserInfo _userInfo;

    public int CountKills
    {
        get
        {
            return _countKills;
        }
    }

    public UserInfo UserInfo
    {
        get
        {
            return _userInfo;
        }
    }

    public bool IsCompleted
    {
        get
        {
            return KillsCurrent >= CountKills;
        }
    }

    public int KillsCurrent { get; private set; }

    private void OnEnable()
    {
        UserInfo.OnChangeKillsCount += OnChangeKillsCountHandler;
    }

    private void OnDisable()
    {
        if (UserInfo != null)
        {
            UserInfo.OnChangeKillsCount -= OnChangeKillsCountHandler;
        }
    }

    private void OnChangeKillsCountHandler()
    {
        ++KillsCurrent;
    }
}
