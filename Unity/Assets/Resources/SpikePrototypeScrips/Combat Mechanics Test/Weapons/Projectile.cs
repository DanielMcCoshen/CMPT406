using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float force;
    public float destroyTimer = 10;

    void Start()
    {
        Destroy(gameObject, destroyTimer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Projectile")
        {
            
            if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                if (collision.gameObject.tag == "Hydra")
                {
                    var damage = System.Math.Abs(this.force / 50.0f);
                    var hydraHealth = collision.gameObject.GetComponent<HydraBehavior>();
                    hydraHealth.SetHealth(damage);
                }
                else
                {
                    Vector3 dir = this.transform.position - collision.gameObject.transform.position;
                    dir = -dir.normalized;
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(dir.x, dir.y, transform.position.z) * force);
                }
            }        
            Destroy(gameObject);
        }
        
    }


}
