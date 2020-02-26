using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyBehavior : MonoBehaviour
{
    [SerializeField]
    private int jumpTimer;
    private bool waitingForJump;
    public GameObject player;


    private void Jump(GameObject player)
    {
        Debug.Log("JUMP");

        // Where the skelly is going to jump to
        /*
        var destination_x = player.transform.position.x;
        var destination_y = player.transform.position.y;
        */
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
    }

    // Update is called once per frame
    void Update()
    {
        if(waitingForJump == false)
        {
            JumpCheck();
        }
    }
}
