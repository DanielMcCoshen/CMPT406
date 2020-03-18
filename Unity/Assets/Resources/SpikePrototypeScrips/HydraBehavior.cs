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
    public HydraControl hydraControl;
    public float snipeProjectileForce;
    public float bigProjectileForce;
    public float spreadProjectileForce;

    private enum states { NOTHING, SHOOTING };
    private states currentState;
    

    void Start()
    {
        currentState = states.NOTHING;
        health = 100.0f;
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
        if(hydraControl.currentAttackPattern == HydraControl.attackPattern.SNIPE)
        {
            GameObject projectile = Instantiate(snipeProjectilePrefab, position, rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(projectile.transform.up * snipeProjectileForce, ForceMode2D.Impulse);
        }

        else if (hydraControl.currentAttackPattern == HydraControl.attackPattern.BIG)
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
        if(UnityEngine.Random.Range(0.0f, 10.0f) >= hydraControl.hydraHeads)
        {
            ShootFireball(firePoint.position, aimPoint.rotation);
        }
        currentState = states.NOTHING;
    }

    public void SetHealth(float damage)
    {
        this.health -= damage;
        if(this.health <= 0)
        {
            healthBar.SetSize(0);
            if(hydraControl == null)
            {
                Debug.Log("hydraControl not found");
            }
            hydraControl.hydraHeads -= 1;
            if(hydraControl.hydraHeads == 3)
            {
                hydraControl.setAttackPattern(HydraControl.attackPattern.BIG);
                Debug.Log("Attack pattern changed");
            }
            if(hydraControl.hydraHeads <= 0)
            {
                hydraControl.menuManager.HydraDefeated();
            }
            Destroy(gameObject);
        }

        else
        {
            healthBar.SetSize(health / 100);
        }
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetHealth(100.0f);
        }
    }
}
