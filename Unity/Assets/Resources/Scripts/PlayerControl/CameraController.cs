using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variables

    [Header("Player Follow Settings")]
    public Transform player;
    public float smooth = 0.3f;
    public float playerCameraSize;


    [Header("Hydra Fight Postioning")]
    public Vector2 bossRoomPos;
    public float bossFightZoomSpeed;
    public float bossFightCameraSize;

    [Header("Argus Fight Postioning")]
    public Vector2 argusRoomPos;
    public float argusFightZoomSpeed;
    public float argusFightCameraSize;

    private Vector3 velocity = Vector3.zero;
    public bool InBossRoom { get; set; } = false;
    public bool InArgusRoom { get; set; } = false;

    private Camera cam;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }
    void Update()
    {
        Vector3 pos;
        if (!InBossRoom)
        {
            pos = new Vector3(player.position.x, player.position.y, transform.position.z);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, playerCameraSize, bossFightZoomSpeed * Time.deltaTime);
            transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
        }
        else
        {
                //pos = new Vector3(bossRoomPos.x, bossRoomPos.y, transform.position.z);
                pos = new Vector3(player.position.x, player.position.y, transform.position.z);
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, bossFightCameraSize, bossFightZoomSpeed * Time.deltaTime);
                //transform.position = Vector3.Lerp(transform.position, pos, bossFightZoomSpeed * Time.deltaTime);
                transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
            
        }
       
    }

}
