using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBehavior : MonoBehaviour
{
    public GameObject head;

    //Projectile prefabs
    [SerializeField]
    private GameObject purpleProjectile, redProjectile;

    //Speed for projectiles in each state
    [SerializeField]
    private float normalSpeed, damagedSpeed, closedSpeed, redSpeed;

    //Current state, determines attack cooldown and projectile speed
    [HideInInspector]
    public enum State { NORMAL, DAMAGED, CLOSED, RED }
    private State currentState;



    // Start is called before the first frame update
    void Start()
    {
        //Starting state
        currentState = State.NORMAL;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        switch (currentState)
        {
            case State.NORMAL:
                if

        }
        */
    }

    private IEnumerator Shoot(float seconds, GameObject projectile, float projectileForce)
        /**
         * Method for firing projectiles
         *      float seconds: How long to wait before firing
         *      GameObject projectile: Which projectile to fire
         *      float projectileForce: How fast the projectile should move
         *      */
    {
        yield return new WaitForSeconds(seconds);
        //Create the projectile
        GameObject newProjectile = Instantiate(projectile, gameObject.transform);
        //Get the rigidbody for the new projectile
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        //Add the appropriate force to that rigidbody
        rb.AddForce(newProjectile.transform.up * projectileForce, ForceMode2D.Impulse);
    }

    public void ChangeState(State newState)
        /**
         * Method for changing the state of the eye
         * */
    {
        currentState = newState;
    }
}
