using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variables

    public Transform player;
    public float smooth = 0.3f;

    private Vector3 velocity = Vector3.zero;

    // Methods
    void Update()
    {
        // Movement functionality
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.y = player.position.y;
        // Don't change the z.
        pos.z = this.transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);

        // Zoom functionality
        // Likely use camera scaling instead of the z-axis.
    }

}
