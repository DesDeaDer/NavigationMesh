using UnityEngine.SceneManagement;

public static class SceneManagerExtensions {
    public static void LoadScene(this SceneID id)
        => SceneManager.LoadScene((int)id);
}
