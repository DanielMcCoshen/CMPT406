using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBehavior : MonoBehaviour
{
    //Current state of the boss, determines what the eyes should be doing
    [HideInInspector]
    public enum State { NORMAL, SHUDDERING, SPECIAL}
    private State currentState;

    //Tracking health
    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    //Tracking the eyes on the boss
    private List<EyeBehavior> eyes;
    private List<EyeBehavior> activeEyes;
    private List<EyeBehavior> inactiveEyes;

    // Start is called before the first frame update
    void Start()
    {
        //Set starting health
        maxHealth = 1000.0f;
        currentHealth = maxHealth;

        //Set starting state
        currentState = State.NORMAL;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddEye(EyeBehavior eye)
        /**
         * Method for adding eyes to the boss. This lets the boss control their states
         * */
    {
        eyes.Add(eye);
        activeEyes.Add(eye);
    }

    public float GetHealth()
        /**
         * Method for obtaining current health of the head
         *      return: the current health of the head
         *      */
    {
        return currentHealth;
    }

    public void SetHealth(float health)
        /**
         * Method for setting the current health value
         *      float health: the value currentHealth should be set to
         *      */
    {

        //Need to check if the new health value would be above the max health or below 0, and set the currentHealth accordingly
        if (health >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (health <= 0.0f)
        {
            currentHealth = 0.0f;
        }
        else
        {
            if(currentHealth >= 500 && health < 500)
            {
                currentState = State.SHUDDERING;
                Shudder(1.0f);
            }
            currentHealth = health;
        }
    }

    public void SetState(State newState)
        /**
         * Set the current state of the boss
         *      State newState: the new state for the boss
         *      */
    {
        currentState = newState;
    }

    private IEnumerator Shudder(float seconds)
    {
        //Make the boss shudder for that many seconds
        // [ADD CODE HERE]

        //Set the state to the special attack state
        yield return new WaitForSeconds(seconds);
        SetState(State.SPECIAL);
    }

    public void DamageEye()
        /**
         * Method for randomly damaging one of the eyes (changing its state)
         * */
    {

    }
}
