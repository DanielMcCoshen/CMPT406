using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBehavior : MonoBehaviour
{
    public HeadBehavior head;

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

    //Used to see if it can fire another shot yet
    private bool shooting;

    //Sprites
    public Sprite[] sprites;



    // Start is called before the first frame update
    void Start()
    {
        //Starting state
        currentState = State.NORMAL;

        //Add this eye to the central heads eye list
        head = GameObject.FindWithTag("Head").GetComponent<HeadBehavior>();
        if(head != null)
        {
            head.AddEye(this);
        }
        else
        {
            Debug.LogError("Head not found.");
        }

        //Get the sprites for the eyes
        sprites = Resources.LoadAll<Sprite>("Sprites/Bosses/Argus/Eyes");
    }

    // Update is called once per frame
    void Update()
    {
        if(shooting == false)
        {
            switch (currentState)
            {
                //If the eye is normal
                case State.NORMAL:
                    //And the head is normal
                    if (head.GetState() == HeadBehavior.State.NORMAL)
                    {
                        //And the random chance is good
                        if (Random.Range(0.0f, 100.0f) <= 25.0f)
                        {
                            //Fire a shot
                            shooting = true;
                            StartCoroutine(Shoot(Random.Range(2.0f, 4.0f), purpleProjectile, normalSpeed));
                        }
                    }
                    //If the head is shuddering
                    else if (head.GetState() == HeadBehavior.State.SHUDDERING)
                    {
                        //Do nothing
                        break;
                    }
                    //If the head is doing its special attack
                    else if (head.GetState() == HeadBehavior.State.SPECIAL)
                    {
                        //And if the random chance is good
                        if (Random.Range(0.0f, 100.0f) <= 25.0f)
                        {
                            //Fire a shot on a much lower cooldown
                            shooting = true;
                            StartCoroutine(Shoot(Random.Range(0.25f, 0.75f), purpleProjectile, normalSpeed));
                        }
                    }
                    //If the head is in none of the above states
                    else
                    {
                        //Something is wrong
                        Debug.LogError("Invalid state");
                    }
                    break;

                //If the eye is damaged
                case State.DAMAGED:
                    //And the head is normal
                    if (head.GetState() == HeadBehavior.State.NORMAL)
                    {
                        //And the random chance is good
                        if (Random.Range(0.0f, 100.0f) <= 25.0f)
                        {
                            //Fire a shot
                            shooting = true;
                            StartCoroutine(Shoot(Random.Range(3.0f, 5.0f), purpleProjectile, damagedSpeed));
                        }
                    }
                    //If the head is shuddering
                    else if (head.GetState() == HeadBehavior.State.SHUDDERING)
                    {
                        //Do nothing
                        break;
                    }
                    //If the head is doing its special attack
                    else if (head.GetState() == HeadBehavior.State.SPECIAL)
                    {
                        //And if the random chance is good
                        if (Random.Range(0.0f, 100.0f) <= 25.0f)
                        {
                            //Fire a shot on a much lower cooldown
                            shooting = true;
                            StartCoroutine(Shoot(Random.Range(0.25f, 0.75f), purpleProjectile, damagedSpeed));
                        }
                    }
                    //If the head is in none of the above states
                    else
                    {
                        //Something is wrong
                        Debug.LogError("Invalid state");
                    }
                    break;

                //If the eye is red
                case State.RED:
                    //And the head is normal
                    if (head.GetState() == HeadBehavior.State.NORMAL)
                    {
                        //And the random chance is good
                        if (Random.Range(0.0f, 100.0f) <= 50.0f)
                        {
                            //Fire a shot
                            shooting = true;
                            StartCoroutine(Shoot(Random.Range(2.0f, 3.0f), redProjectile, redSpeed));
                        }
                    }
                    //If the head is shuddering
                    else if (head.GetState() == HeadBehavior.State.SHUDDERING)
                    {
                        //Do nothing
                        break;
                    }
                    //If the head is doing its special attack
                    else if (head.GetState() == HeadBehavior.State.SPECIAL)
                    {
                        //And if the random chance is good
                        if (Random.Range(0.0f, 100.0f) <= 50.0f)
                        {
                            //Fire a shot on a much lower cooldown
                            shooting = true;
                            StartCoroutine(Shoot(Random.Range(0.25f, 0.75f), redProjectile, redSpeed));
                        }
                    }
                    //If the head is in none of the above states
                    else
                    {
                        //Something is wrong
                        Debug.LogError("Invalid state");
                    }
                    break;

                //If the eye is closed
                case State.CLOSED:
                    //Do nothing
                    break;

                default:
                    Debug.LogError("Invalid state");
                    break;
            }
        }
        
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
        GameObject newProjectile = Instantiate(projectile, gameObject.GetComponent<AimAtPlayer>().firePoint.transform.position, gameObject.GetComponent<AimAtPlayer>().firePoint.transform.rotation);
        //Get the rigidbody for the new projectile
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        //Add the appropriate force to that rigidbody
        rb.AddForce(newProjectile.transform.up * projectileForce, ForceMode2D.Impulse);

        shooting = false;
    }

    public void SetState(State newState)
        /**
         * Method for changing the state of the eye
         * */
    {
        //Update the state, then update the sprite renderer to show the correct sprite
        currentState = newState;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if(newState == State.NORMAL)
        {
            spriteRenderer.sprite = sprites[2];
        }
        else if(newState == State.DAMAGED)
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if(newState == State.CLOSED)
        {
            spriteRenderer.sprite = sprites[0];
        }
        else if(newState == State.RED)
        {
            spriteRenderer.sprite = sprites[3];
        }
        else
        {
            Debug.LogError("Invalid state");
        }
    }

    public State GetCurrentState()
        /**
         * Method to get the current state of the eye
         * */
    {
        return currentState;
    }
}
