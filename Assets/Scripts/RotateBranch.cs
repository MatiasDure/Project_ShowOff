using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateBranch : InteractableReaction
{
    [SerializeField] private GameObject _branchPivot;

    protected override void Awake()
    {
        base.Awake();

        if (_branchPivot == null) Destroy(this);
    }

    private void Rotate(InteractionInformation info)
    {
        float angle = _branchPivot.transform.rotation.eulerAngles.x;

        angle = angle > 180 ? angle - 360 : angle;

        if(info.KeyPressed == KeyCode.F &&
            angle < 45)
        {
            _branchPivot.transform.Rotate(new(2, 0, 0));
        }
        else if(info.KeyPressed == KeyCode.G &&
            angle > -45)
        {
            _branchPivot.transform.Rotate(new(-2, 0, 0));
        }
    }
    protected override void Interact(InteractionInformation info) => Rotate(info);

    protected override void OnDestroy()
    {
        base.OnDestroy();   
    }
}
