using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyBehavior : MonoBehaviour
{
    [SerializeField]
    private int jumpTimer;
    private bool waitingForJump;
    public GameObject player;
    public Rigidbody2D rb;



    private void Jump(GameObject player)
    {
        //Debug.Log("JUMP");

        // Where the skelly is going to jump to
        
        var destination_x = player.transform.position.x;
        var destination_y = player.transform.position.y;

        Vector2 direction_force = new Vector2();
        direction_force.Set(destination_x - transform.position.x, destination_y - transform.position.y);

        rb.AddForce(direction_force);
        
    }

    private IEnumerator JumpCheck()
    {
        waitingForJump = true;
        yield return new WaitForSeconds(jumpTimer);
        Jump(player);
        waitingForJump = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        waitingForJump = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!waitingForJump)
        {
            StartCoroutine(JumpCheck());
            Debug.Log("Ran JumpCheck");
        }
    }
}
