using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPathProjectile : MonoBehaviour
{
    public Vector3 initialDirection = new Vector3(0f,0f,0f);
    private float angle = -90f;
    private float startTime = 0f;
    public float moveTime;
    public Rigidbody2D rb;
    private float forcePerUnit = 0f;
    public float units;
    private float endTime = 0f;
    public float force;

    public void GiveDirection(Vector3 direction)
    {
        forcePerUnit = 15.384615384615384615384615384615f *2* (5f / moveTime);
        initialDirection = direction;
        startTime = Time.deltaTime;
        endTime = Time.deltaTime + moveTime;
    }

    void Update()
    {
        if (Time.deltaTime < endTime)
        {
            float adj = Time.deltaTime / moveTime;

            angle += (180f * adj);
            Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            rb.AddForce(dir * (forcePerUnit * units * adj));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Projectile")
        {

            if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                Vector3 dir = this.transform.position - collision.gameObject.transform.position;
                dir = -dir.normalized;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(dir.x, dir.y, transform.position.z) * force);
            }

            Destroy(gameObject);
        }

    }
}
