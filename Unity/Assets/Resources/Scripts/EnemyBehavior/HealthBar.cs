using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public Transform bar;

    // Start is called before the first frame update
    private void Start()
    {
        Vector3 heldPosOfBar = bar.transform.position;
        bar.transform.position = new Vector3(heldPosOfBar.x, heldPosOfBar.y, 0);
    }

    // Update is called once per frame
    public void SetSize(float sizeNormalized)
    {
        if(bar != null)
        {
            bar.localScale = new Vector3(sizeNormalized, 1f);
        }
        else
        {
            Debug.Log("HealthBar not found");
        }
    }
}
