using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour, IHittable
{
    public void Hit()
    {
        AudioManager.instance.PlayWithPitch("BalloonPop", 1f);
        gameObject.SetActive(false);
        ScoreSystem.Instance.ModifyScore();
    }
}
