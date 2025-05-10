using UnityEngine;

public class UILoseScreen : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private RectTransform loseScreenWindow;

    private void Awake()
    {
        player.OnPlayerDefeated += Player_OnPlayerDefeated;
        loseScreenWindow.gameObject.SetActive(false);
    }

    private void Player_OnPlayerDefeated(object sender, System.EventArgs e)
    {
        PauseManager.Instance.PauseGame();
        loseScreenWindow.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        player.OnPlayerDefeated -= Player_OnPlayerDefeated;
    }
}
