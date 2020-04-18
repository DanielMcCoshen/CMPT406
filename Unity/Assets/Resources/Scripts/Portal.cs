﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    public string teleportTo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision detected!");
        if (collision.gameObject.tag == "Player")
        {
            if(teleportTo == "Hydra")
            {
                GameObject player = GameObject.FindWithTag("DontDestroy");
                CameraController maincam = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
                maincam.InBossRoom = true;
                DontDestroyOnLoad(player);
                SceneManager.LoadScene("BetterBossFight");
                GameObject.Find("PlayerContainer").transform.position = new Vector2(0, 0);
                GameObject.Find("PlayerContainer").GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            }
            
            else if(teleportTo == "Argus")
            {
                GameObject player = GameObject.FindWithTag("DontDestroy");
                DontDestroyOnLoad(player);
                SceneManager.LoadScene("EyesOfArgus");
                GameObject.Find("PlayerContainer").transform.position = new Vector2(17.0f, 0.0f);
                GameObject.Find("PlayerContainer").GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            }
        }
    }
}