using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthView : ViewBase
{
    [SerializeField] private Image _image;

    public float FillPercent
    {
        set
        {
            _image.fillAmount = value;
        }
    }

    public HealthBehaviour HealthBehaviour { get; set; }

    private void Update()
    {
        FillPercent = (float)HealthBehaviour.Health / HealthBehaviour.HealthMax;
    }

}
