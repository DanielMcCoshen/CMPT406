using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;

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
    private SpriteRenderer spriteRenderer;
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

        void GetMouseInput()
        {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        }

        void GetControllerInput()
        {
            controllerPos = Vector2.right * Input.GetAxisRaw("RHorizontal") + Vector2.up * -Input.GetAxisRaw("RVertical");
        }

        GetDodgeInput();
        GetMovementInput();
        GetMouseInput();
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
                rb.MovePosition(rb.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
                this.spriteRenderer.color = baseColour;
            }
            else
            {
                float step = movementSpeed * 3f;
                rb.MovePosition(rb.position + movementDirection * step * Time.fixedDeltaTime);
                this.spriteRenderer.color = dodgeColour;
            }
        }

        void RotateWithMouse()
        {
            Vector2 lookDirection = mousePos - rb.position;
            float angleOfRotation = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            rb.rotation = angleOfRotation - 90f;
        }

        void RotateWithController()
        {
            if (controllerPos.sqrMagnitude > 0.0f)
            {
                float angleOfRotation = Mathf.Atan2(controllerPos.y, controllerPos.x) * Mathf.Rad2Deg;
                rb.rotation = angleOfRotation - 90f;
            }
        }

        if (!useController)
        {
            RotateWithMouse();
        }

        if (useController)
        {
            RotateWithController();
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
