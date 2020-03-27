using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInAHalfCircle : MonoBehaviour
{

    public float moveTime;

    public float units;

    private float angle = 90f;

    private float forcePerUnit = 0f;
    private float startTime = 0f;

    public Rigidbody2D rb;

    private bool switched = false;
    private bool moving = false;
    private float endTime = 0f;
    private float switchTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.deltaTime;
        forcePerUnit = 15.384615384615384615384615384615f * (5f / moveTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            moving = true;
            endTime = Time.time + moveTime;
        }

        if (Time.time < endTime )
        {
            float adj = Time.deltaTime / moveTime;
            Debug.Log(adj);
            angle += (180f * adj);
            Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            rb.AddForce(dir * (forcePerUnit * units * adj));

        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        
    }
}
