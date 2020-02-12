using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float force;
    // Deletes bullet if it touches a Collider2D that is not the player.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            Debug.Log("REEEEEEEEEEEEE");
            Vector3 dir = this.transform.position - collision.gameObject.transform.position;
            dir = -dir.normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(dir.x, dir.y, transform.position.z) * force);
        }
        
        Destroy(gameObject);
    }

}
