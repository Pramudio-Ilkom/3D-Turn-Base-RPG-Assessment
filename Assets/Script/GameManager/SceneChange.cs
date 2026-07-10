using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene("SceneName");
    }
}