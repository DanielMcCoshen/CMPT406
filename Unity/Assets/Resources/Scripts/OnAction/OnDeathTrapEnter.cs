using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathTrapEnter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnDeathTrapTrigger(string trapType)
    {

    }

    public virtual string NameForDeathTrap()
    {
        return "";
    }
}
