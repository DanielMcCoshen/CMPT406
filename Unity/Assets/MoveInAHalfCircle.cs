using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInAHalfCircle : MonoBehaviour
{

    public float moveTime;

    public float units;

    private float angle = 90f;

    private float forcePerUnit = 0f;

    public Rigidbody2D rb;

    private bool switched = false;
    private bool moving = false;
    private float endTime = 0f;
    private float switchTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        forcePerUnit = 15.384615384615384615384615384615f * (5f / moveTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            moving = true;
            endTime = Time.time + moveTime;
            switchTime = Time.time + moveTime/2;
        }

        if (!switched && Time.time > switchTime)
        {
            switched = true;
            
        }

        if (Time.time < endTime )
        {
            float adj = Time.deltaTime / moveTime;
            Vector3 dir = new Vector3(0, 200f, 0);
            if (switched)
            {
                rb.AddForce(dir * adj * -1);
            }
            else
            {
                rb.AddForce(dir * adj);
            }
            
        }
        
        
    }
}
