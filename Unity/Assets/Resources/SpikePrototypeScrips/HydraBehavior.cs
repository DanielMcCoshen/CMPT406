using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class HydraBehavior : MonoBehaviour
{
    public float shootWaitTime = 2f;
    private float angle = 0f;
    private float forcePerUnit = 0f;
    public float health;
   
    private bool vertical = false;
    private bool switched = false;

    public GameObject player = null;
    public Rigidbody2D rb;

    public Transform aimPoint;
    public Transform firePoint;
    public GameObject snipeProjectilePrefab;
    public GameObject bigProjectilePrefab;
    public GameObject spreadProjectilePrefab;
    public HealthBar healthBar;
    public float snipeProjectileForce;
    public float bigProjectileForce;
    public float spreadProjectileForce;
    private int hydraHeads;

    private enum states { NOTHING, SHOOTING };
    private states currentState;
    public enum attackPattern { SNIPE, BIG, SPREAD }
    public attackPattern currentAttackPattern;
    

    void Start()
    {
        currentState = states.NOTHING;
        health = 100.0f;


        // Replace this with code to get the starting pattern based on votes
        //currentAttackPattern = attackPattern.SNIPE;
        currentAttackPattern = attackPattern.BIG;

        hydraHeads = GameObject.Find("HydraControl").gameObject.GetComponent<HydraControl>().hydraHeads;

        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector2 currentVelocity = rb.velocity;
        Vector2 oppositeForce = -currentVelocity;
        rb.AddRelativeForce(oppositeForce);
        if (this.health <= 0.0f)
        {
            GameObject.Find("HydraControl").GetComponent<HydraControl>().hydraHeads -= 1;
            GameObject.Destroy(this);
        }

        if (hydraHeads <= 0)
        {
            //game over
        }

        switch (currentState)
        {
            case states.NOTHING:
                StartCoroutine(Shoot());
                break;

            case states.SHOOTING:
                break;
        }
    }

    public void setAttackPattern(attackPattern newAttack)
    {
        this.currentAttackPattern = newAttack;
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
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 4.0f));
        if(UnityEngine.Random.Range(0.0f, 10.0f) >= hydraHeads)
        {
            ShootFireball(firePoint.position, aimPoint.rotation);
            health -= 10.0f;
            healthBar.SetSize(health / 100);
        }
        currentState = states.NOTHING;
    }
}
