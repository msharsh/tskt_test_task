using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Loads level by given name
    /// </summary>
    /// <param name="scene"></param>
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
