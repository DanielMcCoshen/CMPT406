using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellhoundBehavior : MonoBehaviour
{

    [Header("Stats")]
    public float moveSpeed;
    public float maxMovementSpeed;
    public float maxVelocityChange;
    public float projectileForce;
    public float attackDistance;
    public float biteRecoveryTime;
    private bool canAttack = false;


    [Header("Object References")]
    public Rigidbody2D rb;
    public Transform aimPoint;
    public Transform firePoint;
    public GameObject projectilePrefab;
    private GameObject player = null;

    // State Machine
    private enum states { NOTHING, CHASING, ATTACKING, RECOVERING };
    private states currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = states.NOTHING;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentVelocity = rb.velocity;
        Vector2 oppositeForce = -currentVelocity;

        void MoveTowardTarget()
        {
            // Do the move thing
            var heading = player.transform.position - this.transform.position;
            var targetVelocity = heading * moveSpeed;
            targetVelocity.x = Mathf.Clamp(targetVelocity.x, -maxMovementSpeed, maxMovementSpeed);
            targetVelocity.y = Mathf.Clamp(targetVelocity.y, -maxMovementSpeed, maxMovementSpeed);

            var velocity = rb.velocity;
            var velocityChange = (new Vector2(targetVelocity.x, targetVelocity.y) - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);

            rb.AddForce(velocityChange);
        }

        void executeStateMachine()
        {
            switch (currentState)
            {
                case states.NOTHING:
                    if (player != null)
                    {
                        currentState = states.CHASING;
                    }
                    break;
                case states.CHASING:
                    MoveTowardTarget();
                    if (Vector3.Distance(player.transform.position, this.transform.position) < attackDistance)
                    {
                        currentState = states.ATTACKING;
                    }
                    break;
                case states.ATTACKING:
                    currentState = states.RECOVERING;
                    if (canAttack == false) {
                        canAttack = true;
                        BiteAttack(firePoint.position, aimPoint.rotation);
                    }
                    break;
                case states.RECOVERING:
                    if (canAttack == true)
                    {
                        canAttack = false;
                        StartCoroutine(WaitToRecover());
                    }
                    break;
            }
        }

        executeStateMachine();

    }

    void BiteAttack(Vector3 position, Quaternion rotation)
    {
        // Stop in place.
        rb.velocity = Vector3.zero;
        // Do the bite thing
        //Debug.Log("Biting");
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);
        Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();
        projRb.AddForce(projectile.transform.up * projectileForce, ForceMode2D.Impulse);
    }

    IEnumerator WaitToRecover()
    {
        //Debug.Log("Recovering");
        yield return new WaitForSeconds(biteRecoveryTime);
        currentState = states.CHASING;
    }

}
