using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Vector3 StartingPosition { get; set; }
    public float Speed { get; set; }
    public Transform Target { get; set; }
    public bool ReachedTarget { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = Target.position - this.transform.position;
        float dist = dir.magnitude;

        //still not there yet
        if(dist > 0.5f)
        {
            this.transform.position += dir.normalized * Speed;
            ReachedTarget = false;
        }
        else ReachedTarget = true;

    }

    public void ResetPosition()
    {
        this.transform.position = StartingPosition;
    }
}
