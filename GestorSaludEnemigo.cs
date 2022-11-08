using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GestorSaludEnemigo : GestorSalud
{
    Trap trapScript;
    BonkHead bonk;
    EnemyMovment enemyMov;
    NavMeshAgent enemyNav;
    Rigidbody rb;
    [SerializeField]
    float enemyDestroyTime = 1f;

    protected override void Start() {
        base.Start();
        trapScript = GetComponent<Trap>();
        bonk = GetComponentInChildren<BonkHead>();
        enemyMov = GetComponent<EnemyMovment>();
        enemyNav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }
    protected override void GameEnd(){
       
        base.GameEnd();
        Destroy(trapScript);
        Destroy(bonk);
        enemyMov.triggerDeath();
        Destroy(enemyMov);
        Destroy(enemyNav);
        rb.isKinematic = true;
        Collider[] enemyCollider = GetComponents<CapsuleCollider>();
        foreach (Collider coll in enemyCollider){
            Destroy(coll);
        }
    }

     protected override IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(0.01f);
       //yield return new WaitUntil(() => sourceOfPain.isPlaying == false);
       // or yield return new WaitWhile(() => audiosource.isPlaying == true);
       Destroy(this.gameObject,enemyDestroyTime);
    }
    
}
