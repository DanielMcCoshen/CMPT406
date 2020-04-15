using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class Test : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(coroutine());
    }

    IEnumerator coroutine()
    {
        while (true)
        {
            string position = "None";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp("http://localhost:5000/game/location");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                position = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            Debug.Log(position);
            switch (position)
            {
                case "centre":
                    gameObject.transform.position = new Vector2(0f, 0f);
                    break;
                case "top":
                    gameObject.transform.position = new Vector2(0f, 1f);
                    break;
                case "bottom":
                    gameObject.transform.position = new Vector2(0f, -1f);
                    break;
                case "left":
                    gameObject.transform.position = new Vector2(-1f, 0f);
                    break;
                case "right":
                    gameObject.transform.position = new Vector2(1f, 0f);
                    break;
                default:
                    Debug.Log("Bad Server Response");
                    break;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
