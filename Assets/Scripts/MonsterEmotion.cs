using UnityEngine;

public abstract class MonsterEmotion : MonoBehaviour
{
    [SerializeField] protected GameObject EmotionIcon;
    [SerializeField] protected string _emotionSound;

    protected void ShowEmotion() => EmotionIcon.SetActive(true);
    protected void HideEmotion() => EmotionIcon.SetActive(false);

    public abstract void AffectMonster();
}
