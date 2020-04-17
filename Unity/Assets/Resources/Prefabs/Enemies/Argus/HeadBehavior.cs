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
    [SerializeField]
    private HealthBar healthBar;
    private float eyeDamageThreshold = 50.0f;

    //Tracking the eyes on the boss
    private List<EyeBehavior> eyes = new List<EyeBehavior>();
    private List<EyeBehavior> activeEyes = new List<EyeBehavior>();
    private List<EyeBehavior> inactiveEyes = new List<EyeBehavior>();

    //Menu manager for setting the victory screen
    [SerializeField]
    private PauseMenu menuManager;

    private int shudderDirection = -1;

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
        if(currentState == State.SHUDDERING)
        {
            transform.Translate(0.1f * shudderDirection, 0, 0);
            shudderDirection *= -1;
        }
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
            healthBar.SetSize(currentHealth / maxHealth);
        }
        else if (health <= 0.0f)
        {
            currentHealth = 0.0f;
            healthBar.SetSize(currentHealth / maxHealth);
            menuManager.BossDefeated();
        }
        else
        {
            //Checks for if the boss should do its threshold attacks, and checks if an eye should be damaged
            if(currentHealth >= 600.0f && health < 600.0)
            {
                currentState = State.SHUDDERING;
                StartCoroutine(SpecialAttack(1.0f, 4.0f));
            }
            else if(currentHealth >= 300.0f && health < 300.0)
            {
                currentState = State.SHUDDERING;
                StartCoroutine(SpecialAttack(1.5f, 6.0f));
            }
            eyeDamageThreshold -= (currentHealth - health);
            if(eyeDamageThreshold <= 0.0f && activeEyes.Count > 0)
            {
                DamageEye();
                eyeDamageThreshold = 50.0f;
            }
            currentHealth = health;
            healthBar.SetSize(currentHealth / maxHealth);
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

    public State GetState()
        /**
         * Method for getting the current state of the boss
         *      return: the current state of the boss
         * */
    {
        return currentState;
    }

    private IEnumerator SpecialAttack(float shudderSeconds, float attackSeconds)
    {
        /**
         * Method for making the boss do its special attack. 
         *      float shudderSeconds: how long the boss shudders before attacking
         *      float attackSeconds: How long the boss should do the attack for
         *      */
        Debug.Log("Initiating special attack");
        Vector3 position = transform.position;
        Color originalColor = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        gameObject.layer = 0;
        yield return new WaitForSeconds(shudderSeconds);
        SetState(State.SPECIAL);
        //Set all inactive eyes to red for the special attack
        AwakeEyes();
        yield return new WaitForSeconds(attackSeconds);
        SetState(State.NORMAL);
        gameObject.layer = 9;
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
        transform.position = position;
        
        Debug.Log("Ending special attack");

    }

    public void DamageEye()
        /**
         * Method for randomly damaging one of the eyes (changing its state)
         * */
    {
        //Randomly select an active eye and move it to the next state of damage. Put it in the appropriate list (damaged to destroyed means it must be removed from the active eyes so it can't be damaged again)
        EyeBehavior selectedEye = activeEyes[Random.Range(0, activeEyes.Count)];
        switch(selectedEye.GetCurrentState())
        {
            case EyeBehavior.State.NORMAL:
                selectedEye.SetState(EyeBehavior.State.DAMAGED);
                Debug.Log("Set eye state to damaged");
                break;
            case EyeBehavior.State.DAMAGED:
                selectedEye.SetState(EyeBehavior.State.CLOSED);
                activeEyes.Remove(selectedEye);
                inactiveEyes.Add(selectedEye);
                break;
            case EyeBehavior.State.RED:
                selectedEye.SetState(EyeBehavior.State.CLOSED);
                activeEyes.Remove(selectedEye);
                inactiveEyes.Add(selectedEye);
                break;
            default:
                Debug.LogError("Invalid state.");
                break;
        }
    }

    public void AwakeEyes()
        /**
         * Method for resurrecting all destroyed eyes as red eyes
         * */
    {
        while(inactiveEyes.Count > 0)
        {
            activeEyes.Add(inactiveEyes[0]);
            inactiveEyes[0].SetState(EyeBehavior.State.RED);
            inactiveEyes.RemoveAt(0);
        }
    }

    public void OnMouseDown()
    {
        Debug.Log("Head hit");
        SetHealth(GetHealth() - 50.0f);
    }
}
