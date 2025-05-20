using UnityEngine;
using UnityEngine.UI;

public class InfoView : ViewBase {
    [SerializeField] private Text _text;

    public string Text {
        set => _text.text = value;
}
