using UnityEngine;

public abstract class MonsterEmotion : MonoBehaviour
{
    [SerializeField] protected GameObject EmotionIcon;

    protected void ShowEmotion() => EmotionIcon.SetActive(true);
    protected void HideEmotion() => EmotionIcon.SetActive(false);
}
