public class DefeadView : ViewBase
{
    //Call from UI
    public void Replay()
    {
        Hide();
        SceneID.Game.LoadScene();
    }

    //Call from UI
    public void Back()
    {
        Hide();
        SceneID.Menu.LoadScene();
    }
}
