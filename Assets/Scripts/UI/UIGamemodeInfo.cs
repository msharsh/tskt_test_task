using TMPro;
using UnityEngine;

public class UIGamemodeInfo : MonoBehaviour
{
    [SerializeField] private RectTransform gamemodeInfoWindow;
    [SerializeField] private TextMeshProUGUI infoText;

    private readonly string classicInfo = 
        "\t- Infinite health\n\t- Enemies spawn on button click";

    private readonly string survivalInfo =
        "\t- Limited health\n\t- Game ends after player loses all of his health\n\t- Enemies spawn continuously";

    public void ShowGamemodeClassicInfo()
    {
        gamemodeInfoWindow.gameObject.SetActive(true);
        infoText.text = classicInfo;
    }

    public void ShowGamemodeSurvivalInfo()
    {
        gamemodeInfoWindow.gameObject.SetActive(true);
        infoText.text = survivalInfo;
    }

    public void HideGamemodeInfo()
    {
        gamemodeInfoWindow.gameObject.SetActive(false);
    }
}
