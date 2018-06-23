using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : ViewBase
{
    [SerializeField] private Image _image;
    [SerializeField] private HealthBehaviour _healthBehaviour;

    public float FillPercent
    {
        set
        {
            _image.fillAmount = value;
        }
    }

    public HealthBehaviour HealthBehaviour
    {
        get
        {
            return _healthBehaviour;
        }
    }

    private void Update()
    {
        FillPercent = (float)HealthBehaviour.Health / HealthBehaviour.HealthMax;
    }

}
