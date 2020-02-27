using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveForce : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform end;
    [SerializeField]
    private float speed;

    private Vector2 towardEnd;
    private Vector2 upward;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        towardEnd = Vector3.Normalize(end.position - start.position);
        upward = Vector3.Normalize(Quaternion.Euler(0, 0, 90) * towardEnd);

        //rb.AddForce(towardEnd * speed * Time.deltaTime);
        //rb.AddForce(-upward * speed * Time.deltaTime);

        direction = upward;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(direction * speed * Time.deltaTime);

        direction = Vector3.Normalize(Quaternion.Euler(0, 0, -1) * direction);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, towardEnd);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, upward);    
    }
}
