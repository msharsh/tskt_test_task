using UnityEngine;

public class UIPauseScreen : MonoBehaviour
{
    [SerializeField] private RectTransform pauseWindow;
    [SerializeField] private RectTransform pauseButton;

    private void Awake()
    {
        HidePauseScreen();
    }

    public void ShowPauseScreen()
    {
        pauseWindow.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    public void HidePauseScreen()
    {
        pauseWindow.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }
}
