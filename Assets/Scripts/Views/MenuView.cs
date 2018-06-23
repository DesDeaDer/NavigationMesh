using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuView : ViewBase
{
    [SerializeField] private int _gameSceneID;

    public void Play()
    {
        SceneManager.LoadScene(_gameSceneID);
    }
}
