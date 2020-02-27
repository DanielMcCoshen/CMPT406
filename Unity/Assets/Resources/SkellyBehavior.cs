using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyBehavior : MonoBehaviour
{
    /*
    [SerializeField]
    private int jumpTimer;

    public GameObject player;
    public Rigidbody2D rb;

    [SerializeField]
    private float speed;

    private Vector2 destination = new Vector2();
    private bool jumping = false;
    private float distanceToPlayer;


    private IEnumerator JumpCheck()
    {
        jumping = true;
        yield return new WaitForSeconds(jumpTimer);
        destination.Set(player.transform.position.x, player.transform.position.y);
        Debug.Log("setting destination");
        jumping = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        jumping = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(jumping);

        if (!jumping)
        {
            StartCoroutine(JumpCheck());
            //Debug.Log("Ran JumpCheck");
        }

        else
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, destination, speed));
            if (Vector2.Distance(transform.position, destination) < 4)
            {
                jumping = false;
            }
        }
    }
    */

    [SerializeField]
    private int jumpTimer;

    [SerializeField]
    private float speed;

    public GameObject player;
    public Rigidbody2D rb;

    private Vector2 destination;
    private enum states { NOTHING, SHOOT_WAITING, SHOOTING, JUMP_WAITING, JUMPING };
    private states currentState;

    void Start()
    {
        currentState = states.NOTHING;
    }

    void Update()
    {
        Debug.Log(currentState);
        //Debug.Log(Vector2.Distance(transform.position, destination));
        switch (currentState)
        {
            case states.NOTHING:
                StartCoroutine(Jump());
                break;

            case states.SHOOT_WAITING:
            

            case states.SHOOTING:
                Debug.Log("BANG");
                currentState = states.NOTHING;
                break;

            case states.JUMP_WAITING:
                

            case states.JUMPING:
                rb.MovePosition(Vector2.MoveTowards(transform.position, destination, speed));
                if(Vector2.Distance(transform.position, destination) < 1)
                {
                    StartCoroutine(Shoot());
                }
                break;

        }
    }

    private IEnumerator Shoot()
    {
        currentState = states.SHOOT_WAITING;
        yield return new WaitForSeconds(5);
        currentState = states.SHOOTING;
    }

    private IEnumerator Jump()
    {
        currentState = states.JUMP_WAITING;
        yield return new WaitForSeconds(5);
        destination = new Vector2(player.transform.position.x, player.transform.position.y);
        currentState = states.JUMPING;
    }
}
