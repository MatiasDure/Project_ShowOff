using UnityEngine;

public abstract class MonsterEmotion : MonoBehaviour
{
    [SerializeField] protected GameObject EmotionIcon;
    [SerializeField] protected string _emotionSound;

    protected void ShowEmotion()
    {
        if(EmotionIcon != null) EmotionIcon.SetActive(true);
    }
    protected void HideEmotion()
    {
        if(EmotionIcon != null) EmotionIcon.SetActive(false);
    }

    public abstract void AffectMonster();
}
