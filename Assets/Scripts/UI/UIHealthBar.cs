using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image healthBar;

    private void Awake()
    {
        player.OnHealthChanged += Player_OnHealthChanged;
    }

    private void Player_OnHealthChanged(object sender, Player.OnHealthChangedEventArgs e)
    {
        if (player.GetMaxHealth() != 0)
            healthBar.fillAmount = e.newHealth / player.GetMaxHealth();
        else
            healthBar.fillAmount = 0;
    }
}
