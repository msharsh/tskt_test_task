using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private RectTransform gamemodeSelector;

    public void ToggleGamemodeSelector(bool enable)
    {
        gamemodeSelector.gameObject.SetActive(enable);
    }

    /// <summary>
    /// Exits from game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
