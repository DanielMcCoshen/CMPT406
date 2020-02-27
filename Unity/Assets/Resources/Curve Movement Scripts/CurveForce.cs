using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveForce : MonoBehaviour
{
    [Header("Vector References")]
    Vector3 midpoint;
    Vector3 start;
    Vector3 end;

    [Header("Movement Variables")]
    [SerializeField]
    float speed = 5f;
    private bool canMove = false;
    [SerializeField]
    private bool clockwise = true;

    [Header("Rigidbody Reference")]
    [SerializeField]
    Rigidbody2D rb;

    private void Start() {
        if (rb == null) {
            rb = this.transform.GetComponent<Rigidbody2D>();
            Debug.LogError("Set the rigidbody in the editor for: " + this.ToString());
        }
    }

    private void Update() {
        Rotate();
    }

    /**
     * Rotates the object around the midpoint.
     */
    private void Rotate() {
        float GetDirectionalSpeed() {
            if (clockwise) {
                return -speed;
            } else {
                return speed;
            }
        }

        void MoveAroundMidpoint() {
            if (canMove) {
                Quaternion q = Quaternion.AngleAxis(GetDirectionalSpeed(), transform.forward);
                rb.MovePosition(q * (rb.transform.position - midpoint) + midpoint);
                rb.MoveRotation(rb.transform.rotation * q);
            }
        }

        MoveAroundMidpoint();

    }

    /**
     * Sets up the location to rotate around and begins the projectile's movement.
     */
    public void Setup(Vector3 firePoint, Vector3 target) {
        start = firePoint;
        rb.transform.position = start;
        end = target;
        midpoint = (start + end)/2;
        canMove = true;
    }
}
