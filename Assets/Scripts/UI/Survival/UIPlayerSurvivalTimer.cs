using TMPro;
using UnityEngine;

public class UIPlayerSurvivalTimer : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI timerText;

    private float survivalTime;

    private void Awake()
    {
        player.OnPlayerDefeated += Player_OnPlayerDefeated;
    }

    private void Update()
    {
        timerText.text = TimeToString(survivalTime);
        survivalTime += Time.deltaTime;
    }

    public static string TimeToString(float time)
    {
        string minutes = ((int)(time / 60)).ToString("00");
        string seconds = ((int)(time % 60)).ToString("00");
        return minutes + ":" + seconds;
    }

    private void Player_OnPlayerDefeated(object sender, System.EventArgs e)
    {
        float highscore = Mathf.Max(PlayerPrefs.GetFloat("highscore"), survivalTime);
        PlayerPrefs.SetFloat("highscore", highscore);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        player.OnPlayerDefeated -= Player_OnPlayerDefeated;
    }
}
