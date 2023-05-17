using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathTorch : ToggleObject
{
    public event Action<PathTorch> OnTorchEnable;
    public event Action<PathTorch> OnTorchDisable;

    InteractionInformation info; // Dummy info
    protected override void Interact(InteractionInformation pInfo)
    {
        info = pInfo;
        OnTorchEnable?.Invoke(this);
    }

    public void EnableTorch()
    {
        base.Interact(info);
    }
}
