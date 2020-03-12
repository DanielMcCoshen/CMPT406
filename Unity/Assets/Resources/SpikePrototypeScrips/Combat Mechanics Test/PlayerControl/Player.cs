using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float maxVelocityChange = 100f;
    public float maxMovementSpeed;
    public bool useController;

    [Header("Dodge Variables")]
    private bool dodging;
    [SerializeField]
    private float dodgeSpeedMultiplication = 3f;
    [SerializeField]
    private float dodgeLength = 0.3f;
    [SerializeField]
    private Color dodgeColour = new Color(255,255,255);

    [Header("Sprite Variables")]
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;
    private Color baseColour;

    [Header("Object References")]
    public Rigidbody2D rb;
    public Camera mainCam;

    Vector2 movementDirection;
    Vector2 mousePos;
    Vector2 controllerPos;
    

    private void Start()
    {
        baseColour = spriteRenderer.color;
        maxVelocityChange = 3f * movementSpeed;
        if (spriteRenderer == null)
        {
            Debug.LogError("Sprite Renderer not set in editor");
        }
    }

    void Update()
    {
        if (!dodging)
        {
            GetInput();
        }
    }

    void FixedUpdate()
    {
        DoMovement();
    }

    /**
     * Gets the input from the mouse and keyboard and controller.
     * Does not gather input if the user is dodging.
     */
    void GetInput()
    {
        void GetDodgeInput()
        {
            if ((Input.GetButtonDown("Jump")) && movementDirection != Vector2.zero)
            {
                //Debug.Log("Dodge key pressed.");
                dodging = true;
                StartCoroutine(EndDash());
            }
        }

        void GetMovementInput()
        {
            movementDirection.x = Input.GetAxisRaw("Horizontal");
            movementDirection.y = Input.GetAxisRaw("Vertical");
        }

        void GetControllerInput()
        {
            controllerPos = Vector2.right * Input.GetAxisRaw("RHorizontal") + Vector2.up * -Input.GetAxisRaw("RVertical");
        }

        GetDodgeInput();
        GetMovementInput();
        GetControllerInput();
    }

    /**
     * Moves and rotates the player from user input.
     */
    void DoMovement()
    {
        void MoveCharacter()
        {
            if (!dodging)
            {

              
                var targetVelocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                targetVelocity = transform.TransformDirection(targetVelocity);
                targetVelocity *= movementSpeed*10 * Time.fixedDeltaTime;
                targetVelocity.x = Mathf.Clamp(targetVelocity.x, -maxMovementSpeed, maxMovementSpeed);
                targetVelocity.y = Mathf.Clamp(targetVelocity.y, -maxMovementSpeed, maxMovementSpeed);

                // Apply a force that attempts to reach our target velocity
                var velocity = rb.velocity;
                var velocityChange = (new Vector2(targetVelocity.x, targetVelocity.y) - velocity);


                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange)*.5f;
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
                    directionFromInputs(movementDirection.y+ rb.position.y, rb.position.y));
                targetVelocity *= step * Time.fixedDeltaTime;
                Debug.Log(targetVelocity.x);
                Debug.Log(targetVelocity.y);
                // Apply a force that attempts to reach our target velocity
                var velocity = rb.velocity;
                var velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
                rb.AddForce(velocityChange);
                this.spriteRenderer.color = dodgeColour;
            }
        }

        MoveCharacter();
    }

    /**
     * Ends the dodge when it's length is exceeded.
     */ 
    IEnumerator EndDash()
    {
        yield return new WaitForSeconds(dodgeLength);
        //Debug.Log("Dodge end.");
        dodging = false;
    }
}
