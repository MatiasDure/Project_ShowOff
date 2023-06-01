using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathTorch : ToggleObject
{
    public event Action<PathTorch> OnTorchEnable;
    public event Action<PathTorch> OnTorchDisable;

    InteractionInformation info; // Dummy info

    //Nico 
    private static int torchCount = 0;

    protected override void Interact(InteractionInformation pInfo)
    {
        info = pInfo;
        OnTorchEnable?.Invoke(this);

        //Nico
        
        

       
    }

    public void EnableTorch()
    {
        base.Interact(info);
        //Nico
        torchCount++;
        float pitch = 1.0f + (torchCount * 0.1f); // Adjust the pitch increment as needed
        float pitchKey = 0.5f + ((torchCount - 1) * 2.0f) / 12f;
        AudioManager.instance.PlayWithPitch("LightUp", 1);
        if(pitchKey <= 3)
        {
            AudioManager.instance.PlayWithPitch("KeyNote", pitchKey);
        }
        else
        {
            AudioManager.instance.PlayWithPitch("FinalNote", 1);
        }
        
    }
}
