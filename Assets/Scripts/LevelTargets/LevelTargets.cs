using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelTargets : MonoBehaviour, ITargetLevel {
    [SerializeField] private Transform _levelConfiguration;

    IDictionary<Type, ITargetLevel> _targets;
    public IEnumerable<ITargetLevel> Targets 
        => (_targets ??= _levelConfiguration
                .GetComponentsInChildren<ITargetLevel>()
                .ToDictionary(x => x.GetType()))
            .Values;

    public T Get<T>() where T : ITargetLevel
        => (T)_targets[typeof(T)];

    public bool IsCompleted 
        => Targets.All(x => x.IsCompleted);
}
