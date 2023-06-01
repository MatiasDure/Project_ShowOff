using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFearTheme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayWithPitch("ThemeFear", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
