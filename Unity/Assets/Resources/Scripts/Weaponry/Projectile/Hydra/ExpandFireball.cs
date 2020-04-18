using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandFireball : MonoBehaviour
{
    private Transform projectileTransform;
    private Projectile projectile;

    public float maxSize;

    // Start is called before the first frame update
    void Start()
    {
        maxSize = 2.0f;
        projectileTransform = gameObject.GetComponent<Transform>();
        projectile = gameObject.GetComponent<Projectile>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (transform.localScale.x <= maxSize)
        {
            projectileTransform.localScale *= 1.03f;
        }
        projectile.force += 3f;
    }
}