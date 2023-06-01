using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecipeStep
{
    public void FreezeGameState();

    public void UnFreezeGameState();
}
