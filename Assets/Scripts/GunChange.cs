using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChange : InteractableReaction , IPickable
{
    [SerializeField] Transform _currentGunInStation;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (transform.childCount < 1) return;
            
        _currentGunInStation = transform.GetChild(0);
    }

    public void PickedUp(GameObject objInteracted)
    {
        if (!objInteracted.TryGetComponent<GunPickUp>(out GunPickUp pickUpScript)) return;

        //give stationed gun to player
        _currentGunInStation.transform.SetParent(pickUpScript.PickUpJoint);
        _currentGunInStation.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        //Set player gun in station
        Transform oldPlayerGun = pickUpScript.CurrentHoldingGun;
        oldPlayerGun.SetParent(this.transform);
        oldPlayerGun.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        //exchange guns
        pickUpScript.CurrentHoldingGun = _currentGunInStation;
        _currentGunInStation = oldPlayerGun;
    }

    protected override void Interact(InteractionInformation obj)
    {
        PickedUp(obj.ObjInteracted);
    }
}
