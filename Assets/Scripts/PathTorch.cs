using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathTorch : ToggleObject
{
    public event Action<PathTorch, Vector2> OnTorchEnable;

    [SerializeField] Vector2 MonsterPosition;

    InteractionInformation info; // Dummy info

    //Nico 
    private static int torchCount = 0;

    protected override void Interact(InteractionInformation pInfo)
    {
        info = pInfo;
        Vector2 monsterPos = new Vector2(transform.position.x + MonsterPosition.x, transform.position.z + MonsterPosition.y);
        OnTorchEnable?.Invoke(this, monsterPos);

        //Nico




    }

    public void EnableTorch()
    {
        base.Interact(info);
        DisableInteractable();

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

    // Display the position that the monster will take when the torch lights up.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(new Vector3(transform.position.x + MonsterPosition.x, transform.position.y + 0.5f,
                                    transform.position.z + MonsterPosition.y), Vector3.one);
    }
}
