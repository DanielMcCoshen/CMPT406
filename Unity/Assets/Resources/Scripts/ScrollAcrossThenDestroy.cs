using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollAcrossThenDestroy : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spirt;

    private float initialPosition = 0f;
    private float endPosition = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x > endPosition)
        {
            Destroy(gameObject);
        }
    }

    public void SetVelocityAndLayer(float speed, int layer)
    {
        endPosition = gameObject.transform.position.x + 35f;
        rb.velocity = new Vector3(speed, 0, 0);
        spirt.sortingOrder = layer;
    }
}
