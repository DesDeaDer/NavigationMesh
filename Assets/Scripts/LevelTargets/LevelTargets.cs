using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelTargets : MonoBehaviour, ITargetLevel
{
    [SerializeField] private Transform _levelConfiguration;

    private IDictionary<Type, ITargetLevel> _targets;
    public IEnumerable<ITargetLevel> Targets
    {
        get
        {
            if (_targets == null)
            {
                _targets = _levelConfiguration.GetComponentsInChildren<ITargetLevel>().ToDictionary(x => x.GetType());
            }
            return _targets.Values;
        }
    }

    public T Get<T>() where T : ITargetLevel
    {
        return (T)_targets[typeof(T)];
    }

    public bool IsCompleted
    {
        get
        {
            return Targets.All(x => x.IsCompleted);
        }
    }


}
