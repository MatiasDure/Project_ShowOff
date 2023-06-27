using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunChange : InteractableReaction , IPickable
{
    [SerializeField] GunModel[] _models;
    [SerializeField] MonsterEmotion _monsterEmotion;

    private GunModel _currentModel;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (_models.Length < 1) Destroy(this.gameObject);

        _currentModel = FindModel(GunModel.Types.Monster);

        Toggle(_currentModel.ModelType);
    }

    public void Toggle(GunModel.Types pModel)
    {
        foreach (var gun in _models)
        {
            if (!gun.ModelType.Equals(pModel))
            {
                gun.ModelGun.gameObject.SetActive(false);
                continue;
            }

            _currentModel = gun;
            gun.ModelGun.gameObject.SetActive(true);
        }
    }

    public void PickedUp(GameObject objInteracted)
    {
        GunPickUp gunPick = objInteracted.GetComponentInChildren<GunPickUp>();

        if (gunPick == null) return;

        bool isMonsterType = _currentModel.ModelType == GunModel.Types.Monster;

        //give player current model in station
        gunPick.Toggle(_currentModel.ModelType);

        //get the opposite to ourselves
        Toggle(isMonsterType ? GunModel.Types.Player : GunModel.Types.Monster);

        _monsterEmotion.AffectMonster();
    }

    private GunModel FindModel(GunModel.Types pType)
    {
        foreach (var model in _models)
        {
            if (!model.ModelType.Equals(pType)) continue;

            return model;
        }

        return null;
    }

    protected override void Interact(InteractionInformation obj)
    {
        PickedUp(obj.ObjInteracted);
    }
}

[System.Serializable]
public class GunModel
{
    public enum Types
    {
        Monster,
        Player
    }

    public Transform ModelGun;
    public Types ModelType;
}
