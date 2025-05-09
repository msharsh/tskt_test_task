using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIHitIndication : MonoBehaviour
{
    [SerializeField] private Image hitIndicator;
    [SerializeField] private Player player;
    [SerializeField] private float duration;

    private float originalAlpha = 1.0f;

    private void Awake()
    {
        player.OnPlayerDamaged += Player_OnPlayerDamaged;
        originalAlpha = hitIndicator.color.a;
        SetHitIndicatorAlphaPercent(0);
    }

    private void Player_OnPlayerDamaged(object sender, System.EventArgs e)
    {
        StopAllCoroutines();
        StartCoroutine(ImageBlink());
    }

    private IEnumerator ImageBlink()
    {
        // Alpha from 0 to 1
        float timeElapsed = 0;
        while (timeElapsed < duration / 2)
        {
            SetHitIndicatorAlphaPercent(2 * timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Alpha from 1 to 0
        timeElapsed = 0;
        while (timeElapsed < duration / 2)
        {
            SetHitIndicatorAlphaPercent(1 - 2 * timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        SetHitIndicatorAlphaPercent(0f);
    }

    /// <summary>
    /// Changes alpha of hit indicator image.
    /// </summary>
    /// <param name="percent"></param>
    private void SetHitIndicatorAlphaPercent(float percent)
    {
        Color originalColor = hitIndicator.color;
        hitIndicator.color = new Color(originalColor.r, originalColor.g, originalColor.b, percent * originalAlpha);
    }
}
