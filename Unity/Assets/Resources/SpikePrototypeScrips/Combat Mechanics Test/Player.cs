using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    public float movementSpeed;

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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90f;
    }
}
