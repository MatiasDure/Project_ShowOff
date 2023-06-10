using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectMonster : InteractableReaction
{
    [SerializeField] private MonsterEmotion _monsterEmotion;

    protected override void Awake()
    {
        base.Awake();

        if(_monsterEmotion == null)
        {
            _monsterEmotion = FindObjectOfType<MonsterEmotion>();

            if (_monsterEmotion == null) Debug.LogWarning("No monster emotion found!");
        }
    }

    protected override void Interact(InteractionInformation obj)
    {
        TriggerMonster();
        AudioManager.instance.PlayWithPitch("BookInteract", 1f);
    }

    private void TriggerMonster()
    {
        _monsterEmotion.AffectMonster();
    }
}
