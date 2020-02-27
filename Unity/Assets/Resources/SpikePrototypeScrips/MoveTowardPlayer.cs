using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MoveTowardPlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform playerPosition;
    float maxVelocityChange = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float directionFromInputs(float target, float origin)
        {
            if (target > origin)
            {
                return 1f;
            }
            else if (target < origin)
            {
                return -1f;
            }
            return 0f;
        }
        var targetVelocity = new Vector2(directionFromInputs(playerPosition.position.x, gameObject.transform.position.x),
            directionFromInputs(playerPosition.position.y, gameObject.transform.position.y));
        targetVelocity *= 150f * Time.fixedDeltaTime;

        // Apply a force that attempts to reach our target velocity
        var velocity = rb.velocity;
        var velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
        rb.AddForce(velocityChange);
    }

    
}
