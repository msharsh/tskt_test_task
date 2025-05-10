using TMPro;
using UnityEngine;

public class UIGamemodeInfo : MonoBehaviour
{
    [SerializeField] private RectTransform gamemodeInfoWindow;
    [SerializeField] private TextMeshProUGUI infoText;

    private string classicInfo = 
        "\t- Infinite health\n\t- Enemies spawn on button click";

    private string survivalInfo =
        "\t- Limited health\n\t- Game ends after player loses all of his health\n\t- Enemies spawn continuously\n\t- Highscore: ";

    public void ShowGamemodeClassicInfo()
    {
        gamemodeInfoWindow.gameObject.SetActive(true);
        infoText.text = classicInfo;
    }

    public void ShowGamemodeSurvivalInfo()
    {
        gamemodeInfoWindow.gameObject.SetActive(true);

        string highscore;
        if (PlayerPrefs.HasKey("highscore"))
        {
            highscore = UIPlayerSurvivalTimer.TimeToString(PlayerPrefs.GetFloat("highscore"));
        }
        else
        {
            highscore = "not set yet";
        }

        infoText.text = survivalInfo + highscore;
    }

    public void HideGamemodeInfo()
    {
        gamemodeInfoWindow.gameObject.SetActive(false);
    }
}
