public class DefeadView : ViewBase {
    public void Replay() {
        Hide();
        SceneID.Game.LoadScene();
    }

    public void Back() {
        Hide();
        SceneID.Menu.LoadScene();
    }
}
