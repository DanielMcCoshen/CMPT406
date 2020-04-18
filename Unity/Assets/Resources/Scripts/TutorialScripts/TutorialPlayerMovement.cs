using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerMovement : Player
{
    public bool mayMove = false;
    public bool stop = false;

    protected override void DoMovement()
    {
        void MoveCharacter()
        {
            if (!dodging)
            {


                var targetVelocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                targetVelocity = transform.TransformDirection(targetVelocity);
                targetVelocity *= movementSpeed * 10 * Time.fixedDeltaTime;
                targetVelocity.x = Mathf.Clamp(targetVelocity.x, -maxMovementSpeed, maxMovementSpeed);
                targetVelocity.y = Mathf.Clamp(targetVelocity.y, -maxMovementSpeed, maxMovementSpeed);

                // Apply a force that attempts to reach our target velocity
                var velocity = rb.velocity;
                var velocityChange = (new Vector2(targetVelocity.x, targetVelocity.y) - velocity);


                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange) * .5f;
                rb.AddForce(velocityChange);
                this.spriteRenderer.color = baseColour;
            }
            else
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
                float step = movementSpeed * 3f;
                var targetVelocity = new Vector2(directionFromInputs(movementDirection.x + rb.position.x, rb.position.x),
                    directionFromInputs(movementDirection.y + rb.position.y, rb.position.y));
                targetVelocity *= step * Time.fixedDeltaTime;
                // Apply a force that attempts to reach our target velocity
                var velocity = rb.velocity;
                var velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
                rb.AddForce(velocityChange);
                this.spriteRenderer.color = dodgeColour;
            }
        }
        if (mayMove)
        {
            MoveCharacter();
        }
        else if(stop)
        {
            rb.velocity = new Vector3(0, 0, 0);
            stop = false;
        }
    }

    public void SetPlayersAbilityToMove(bool isPlayerAbleToMove)
    {
        mayMove = isPlayerAbleToMove;
    }
}
