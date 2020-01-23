using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // NOTE: This is the bullet script from my 306 project.
    // We'd likely want to put the damage taking functionality in the enemy's behaviour.
    // Additionally, this bullet script has a homing effect. Probably don't want that either.

    private Transform target;
    [SerializeField]
    public GameObject impactEffect;
    public float speed = 70f;
    public float explosionRadius = 0f;
    public float damage = 25f;

    // Set the target of the bullet.
    public void SelectTarget(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy if the target disappears or is already destroyed.
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        void PointToTarget()
        {
            // Point toward target
            Vector3 direction = target.transform.position - this.transform.position;
            float distanceThisFrame = speed * Time.deltaTime;
            
            // Probably don't use this.
            if (direction.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        }

        PointToTarget();

    }

    /**
     * Handle events derived from bullet contact.
     */
    void HitTarget()
    {
        // Display impact effect.
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 1.5f);

        // Handle bullet type.
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    /**
     * Explosion handling.
     */
    void Explode()
    {
        // Get each enemy in the explosion radius.
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        // Damage all enemies.
        foreach (Collider2D hitObject in hitObjects)
        {
            if (hitObject.tag == "Enemy")
            {
                Damage(hitObject.transform);
            }
        }

    }

    /**
     * Deal damage to enemies.
     */
    void Damage(Transform enemy)
    {
        //enemy.gameObject.GetComponent<Enemy>().TakeDamage(this.damage);
    }

    /**
     * Debug for explosion radius in the editor.
     */
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
