using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerEmotion : MonsterEmotion
{
    // Start is called before the first frame update
    void Start()
    {
        AngerQuest.OnIllegalMove += ShowEmotion;
        MonsterNavMesh.OnReachedNewPosition += HideEmotion;
    }
}
