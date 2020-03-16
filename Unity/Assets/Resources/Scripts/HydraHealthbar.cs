using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraHealthbar : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = new Vector3 (1, (int)(gameObject.GetComponent<HydraBehavior>().health / 100.0f), 1);
    }
}
