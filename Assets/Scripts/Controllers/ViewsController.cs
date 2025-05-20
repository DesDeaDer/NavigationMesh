using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ViewsController : MonoBehaviour {
    [SerializeField] private Transform _canvases;

    public Transform Canvases => _canvases;

    public IDictionary<Type, IView> _views;
    public IDictionary<Type, IView> Views 
        => _views ??= Canvases.GetComponentsInChildren<IView>(true).ToDictionary(view => view.GetType());;

    public T Get<T>() where T : IView
        => (T)Views[typeof(T)];

    public void Show<T>()
        => Views[typeof(T)].Show();
}
