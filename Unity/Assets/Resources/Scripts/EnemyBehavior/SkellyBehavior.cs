using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkellyBehavior : MonoBehaviour
{
    public float shootWaitTime = 1f;
    public float jumpWait;
    public float moveTime;
    private float startTime = 0f;
    public float units;
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
    public AudioSource spearThrowingSound;

    private Vector2 destination;
    private enum states { NOTHING, SHOOT_WAITING, SHOOTING, JUMP_WAITING, INTERMEDIATE, JUMPING };
    private states currentState;

    

    void Start()
    {
        currentState = states.NOTHING;
        forcePerUnit = 15.384615384615384615384615384615f * 2 * (5f / moveTime);
        player = GameObject.FindWithTag("Player");
    }

    public void ThrowSpear(Vector3 position, Quaternion rotation)
    {
        spearThrowingSound.Play();
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(projectile.transform.up * projectileForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        Vector2 currentVelocity = rb.velocity;
        Vector2 oppositeForce = -currentVelocity;
        rb.AddRelativeForce(oppositeForce);

        switch (currentState)
        {
            case states.NOTHING:
                currentState = states.JUMP_WAITING;
                //StartCoroutine(Jump());
                break;

            case states.SHOOT_WAITING:
                StartCoroutine(WaitToShoot());
                currentState = states.INTERMEDIATE;
                break;

            case states.SHOOTING:
                //Debug.Log("BANG");
                currentState = states.INTERMEDIATE;
                ThrowSpear(firePoint.position, aimPoint.rotation);
                break;

            case states.JUMP_WAITING:
                currentState = states.INTERMEDIATE;
                if (Mathf.Abs(transform.position.x - player.transform.position.x) < Mathf.Abs(transform.position.y - player.transform.position.y))
                {
                    vertical = true;
                    destination.x = transform.position.x;
                    if (transform.position.y > player.transform.position.y)
                    {

                        destination.y = player.transform.position.y + 2f;
                    }
                    else
                    {
                        destination.y = player.transform.position.y + 2f;
                    }

                }
                else
                {
                    if (transform.position.x > player.transform.position.x)
                    {

                        destination.x = player.transform.position.x + 2f;
                    }
                    else
                    {
                        destination.x = player.transform.position.x + 2f;
                    }
                    destination.y = transform.position.y;
                }
                StartCoroutine(JumpWaiting());
                break;
                

            case states.JUMPING:
                
                float adj = Time.deltaTime / moveTime;

                if (vertical)
                {
                    Vector3 dir = new Vector3(0, vertForce, 0);
                    if (switched)
                    {
                        rb.AddForce(dir * adj * -1);
                    }
                    else
                    {
                        rb.AddForce(dir * adj);
                    }
                }
                else
                {
                    angle += (180f * adj);
                    Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
                    rb.AddForce(dir * (forcePerUnit * units * adj));
                }
                break;

        }
    }

    private IEnumerator JumpWaiting()
    {

        yield return new WaitForSeconds(jumpWait);
        currentState = states.JUMPING;

        if (vertical)
        {
            float first = moveTime * (2f / 3f);
            float second = moveTime * (1f/3f);
            if (destination.y < player.transform.position.y)
            {
                StartCoroutine(JumpVertical(second,first));
            }
            else
            {
                StartCoroutine(JumpVertical(first,second));
            }
        }
        else
        {
            StartCoroutine(Jump(moveTime));
        }
        
    }

    private IEnumerator Jump(float timeTotal)
    {
        startTime = Time.deltaTime;
        yield return new WaitForSeconds(timeTotal);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        switched = false;
        vertical = false;
        currentState = states.SHOOT_WAITING;
    }

    private IEnumerator JumpVertical(float firstTimer, float secondTimer)
    {
        yield return new WaitForSeconds(firstTimer);
        switched = true;
        StartCoroutine(Jump(secondTimer));
    }

    private IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(shootWaitTime);
        currentState = states.SHOOTING;
        yield return new WaitForSeconds(shootWaitTime);
        currentState = states.NOTHING;
    }

    /*
    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(5f);
        currentState = states.SHOOTING;
    }

    
    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(shoot);
        destination = new Vector2(player.transform.position.x, player.transform.position.y);
        currentState = states.JUMPING;
    }
    
    // Coroutines are actually ass and do not work.  
    */
}
