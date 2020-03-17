using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraControl : MonoBehaviour
{
    public int hydraHeads;
    public enum attackPattern { SNIPE, BIG, SPREAD };
    public attackPattern currentAttackPattern;

    // Start is called before the first frame update
    void Start()
    {
        hydraHeads = 6;

        //set initial attack pattern based on vote
        currentAttackPattern = attackPattern.SNIPE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAttackPattern(attackPattern newAttack)
    {
        currentAttackPattern = newAttack;
        Debug.Log(currentAttackPattern);
    }
}
