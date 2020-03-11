using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HydraBehavior : MonoBehaviour
{
    public float shootWaitTime = 2f;
    private float angle = 0f;
    private float forcePerUnit = 0f;
    public float vertForce = 200f;
   
    private bool vertical = false;
    private bool switched = false;

    public GameObject player = null;
    public Rigidbody2D rb;

    public Transform aimPoint;
    public Transform firePoint;
    public GameObject snipeProjectilePrefab;
    public GameObject bigProjectilePrefab;
    public GameObject spreadProjectilePrefab;
    public float snipeProjectileForce;
    public float bigProjectileForce;
    public float spreadProjectileForce;

    private Vector2 destination;
    private enum states { NOTHING, SHOOTING };
    private states currentState;
    private enum attackPattern { SNIPE, BIG, SPREAD }
    private attackPattern currentAttackPattern;
    

    void Start()
    {
        currentState = states.NOTHING;

        // Replace this with code to get the starting pattern based on votes
        currentAttackPattern = attackPattern.SNIPE;

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
                StartCoroutine(Shoot());
                break;

            case states.SHOOTING:
                break;
        }
    }

    public void ShootFireball(Vector3 position, Quaternion rotation)
    {
        if(currentAttackPattern == attackPattern.SNIPE)
        {
            GameObject projectile = Instantiate(snipeProjectilePrefab, position, rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(projectile.transform.up * snipeProjectileForce, ForceMode2D.Impulse);
        }

        else if (currentAttackPattern == attackPattern.BIG)
        {
            GameObject projectile = Instantiate(bigProjectilePrefab, position, rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(projectile.transform.up * bigProjectileForce, ForceMode2D.Impulse);
        }
    }

    private IEnumerator Shoot()
    {
        currentState = states.SHOOTING;
        yield return new WaitForSeconds(2);
        if(UnityEngine.Random.Range(0.0f, 0.1f) >= 0.5)
        {
            ShootFireball(firePoint.position, aimPoint.rotation);
        }
        currentState = states.NOTHING;
    }
}
