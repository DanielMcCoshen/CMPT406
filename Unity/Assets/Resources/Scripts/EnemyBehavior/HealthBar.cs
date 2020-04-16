using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public Transform bar;
    public Vector3 startingScale;

    // Start is called before the first frame update
    private void Start()
    {
        Vector3 heldPosOfBar = bar.transform.position;
        bar.transform.position = new Vector3(heldPosOfBar.x, heldPosOfBar.y, 0);

        startingScale = this.transform.localScale;
    }

    // Update is called once per frame
    public void SetSize(float sizeNormalized)
    {
        if(bar != null)
        {
            bar.localScale = new Vector3(startingScale.x * sizeNormalized, startingScale.y, startingScale.z);
        }
        else
        {
            Debug.Log("HealthBar not found");
        }
    }
}
