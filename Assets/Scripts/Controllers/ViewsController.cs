using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ViewsController : MonoBehaviour
{
    [SerializeField] private Transform _canvases;

    public Transform Canvases
    {
        get
        {
            return _canvases;
        }
    }

    public IDictionary<Type, IView> _views;
    public IDictionary<Type, IView> Views
    {
        get
        {
            if (_views == null)
            {
                _views = Canvases.GetComponentsInChildren<IView>(true).ToDictionary(view => view.GetType());
            }

            return _views;
        }
    }

    public T Get<T>() where T : IView
    {
        return (T)Views[typeof(T)];
    }

    public void Show<T>()
    {
        Views[typeof(T)].Show();
    }
}
