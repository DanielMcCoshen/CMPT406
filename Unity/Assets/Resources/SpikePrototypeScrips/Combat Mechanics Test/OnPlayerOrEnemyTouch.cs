using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnPlayerOrEnemyTouch : MonoBehaviour
{
    Dictionary<string, bool> entities =new Dictionary<string, bool>();
    public float delaytime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        try
        {
            entities[col.gameObject.GetComponent<OnDeathTrapEnter>().NameForDeathTrap()] = false;
        }catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "DeathPit")
        {
            entities[col.gameObject.GetComponent<OnDeathTrapEnter>().NameForDeathTrap()] = true;
            StartCoroutine(ActivateTrap(col.gameObject));
        }
    }

    IEnumerator ActivateTrap(GameObject entity)
    {
        yield return new WaitForSeconds(delaytime);
        try
        {
            if (entities[entity.GetComponent<OnDeathTrapEnter>().NameForDeathTrap()])
        {
            entity.GetComponent<OnDeathTrapEnter>().OnDeathTrapTrigger("pit");
        }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
        
    }
}
