using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private RectTransform hitIndicator;

    public void ToggleHitIndicator(bool enable)
    {
        hitIndicator.gameObject.SetActive(enable);
    }
}
