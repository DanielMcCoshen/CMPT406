using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HydraBehavior : MonoBehaviour
{
    public float shootWaitTime = 2f;
    private float angle = 90f;
    private float forcePerUnit = 0f;
    public float vertForce = 200f;
   
    private bool vertical = false;
    private bool switched = false;

    public GameObject player = null;
    public Rigidbody2D rb;

    public Transform aimPoint;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileForce;

    private Vector2 destination;
    private enum states { NOTHING, SHOOTING };
    private states currentState;

    

    void Start()
    {
        currentState = states.NOTHING;
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector2 currentVelocity = rb.velocity;
        Vector2 oppositeForce = -currentVelocity;
        rb.AddRelativeForce(oppositeForce);

        switch (currentState)
        {
            case states.NOTHING:
                currentState = states.SHOOTING;
                break;

            case states.SHOOTING:
                break;
        }
    }

    public void ShootFireball(Vector3 position, Quaternion rotation)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(projectile.transform.up * projectileForce, ForceMode2D.Impulse);
    }

    private IEnumerator Shoot()
    {
        currentState = states.SHOOTING;
        yield return new WaitForSeconds(2);
        ShootFireball(firePoint.position, aimPoint.rotation);
        currentState = states.NOTHING;
    }
}
