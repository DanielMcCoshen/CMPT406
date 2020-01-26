using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    public float movementSpeed;

    [Header("Dodge Variables")]
    private bool dodging;
    [SerializeField]
    private string dodgeKey = "space";
    [SerializeField]
    private float dodgeSpeedMultiplication = 3f;
    [SerializeField]
    private float dodgeLength = 0.3f;

    [Header("Object References")]
    public Rigidbody2D rb;
    public Camera mainCam;

    Vector2 movement;
    Vector2 mousePos;

    // Methods

    private void Start()
    {
        if (rb == null)
        {
            Debug.LogError("rb not set in inspector.");
            rb = this.GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        GetMovementData();
    }

    void FixedUpdate()
    {
        DoMovement();
    }

    void GetMovementData()
    {
        if (Input.GetKeyDown(dodgeKey) && !dodging && movement != Vector2.zero)
        {
            //Debug.Log("Dodge key pressed.");
            dodging = true;
            StartCoroutine(EndDash());
        }

        if (!dodging)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    void DoMovement()
    {


        // Move character
        if (!dodging)
        {
            rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
        }
        else
        {
            float step = movementSpeed * 3f;
            rb.MovePosition(rb.position + movement * step * Time.fixedDeltaTime);
        }

        // Point to mouse
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90f;
    }

    IEnumerator EndDash()
    {
        yield return new WaitForSeconds(0.3f);
        //Debug.Log("Dodge end.");
        dodging = false;
    }
}
