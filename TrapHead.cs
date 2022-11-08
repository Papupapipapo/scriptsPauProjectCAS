using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHead : Trap
{
    [SerializeField]
    protected BonkHead potentialBonk;
    
    protected override void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent(out GestorSaludProta saludContacto)){
            saludContacto.RemoveHealth(damageEnemy);
            StartCoroutine(headTimer());
        }
    }
    IEnumerator headTimer(){
        potentialBonk.enabled = false;
        yield return new WaitForSeconds(0.3f);
        potentialBonk.enabled = true;
    }
}
