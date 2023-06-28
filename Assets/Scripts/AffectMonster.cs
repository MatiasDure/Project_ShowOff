using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectMonster : InteractableReaction
{
    [SerializeField] private MonsterEmotion _monsterEmotion;
    [SerializeField] string _affectMonsterSound;

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
        if (_affectMonsterSound == null)
        {
            return;
        }

        AudioManager.instance.PlayWithPitch(_affectMonsterSound, 1f);
    }

    private void TriggerMonster()
    {
        _monsterEmotion.AffectMonster();
    }
}
