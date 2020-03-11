using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandFireball : MonoBehaviour
{
    private Transform projectileTransform;
    private Projectile projectile;

    // Start is called before the first frame update
    void Start()
    {
        projectileTransform = gameObject.GetComponent<Transform>();
        projectile = gameObject.GetComponent<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x <= 5.0f)
        {
            projectileTransform.localScale *= 1.005f;
        }
        projectile.force += 4.0f;
    }
}
