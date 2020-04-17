using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpread : SpreadWeapon
{
    public int numberOfWaves;
    public float timeBetweenWaves;

    public override void FireWeapon()
    {
        
        if (!onCooldown)
        {
            firingSFX.Play();
            FireInASpread();
            if(numberOfWaves > 1)
            {
                StartCoroutine(FireNextSpread(2));
            }
            StartCoroutine(WeaponCooldown());
        }
    }

    public IEnumerator FireNextSpread(int currentTotal)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        FireInASpread();
        if (numberOfWaves > currentTotal)
        {
            StartCoroutine(FireNextSpread(currentTotal+1));
        }
    }
}
