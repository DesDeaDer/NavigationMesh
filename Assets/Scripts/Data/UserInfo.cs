using System;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public event Action<int> OnAddScore;
    public event Action OnChangeKillsCount;

    public int Score { get; private set; }
    public int KillsCount { get; private set; }

    public void AddScore(int value)
    {
        Score += value;

        if (OnAddScore != null)
        {
            OnAddScore(value);
        }
    }

    public void KillEnemy()
    {
        ++KillsCount;

        if (OnChangeKillsCount != null)
        {
            OnChangeKillsCount();
        }
    }
}
