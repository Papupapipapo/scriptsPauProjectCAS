using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointReturn : MonoBehaviour
{
    private Transform currentPoint;
    private GameObject prota;
    private GestorSaludProta protaHP;

    void Start(){
        prota = GameObject.FindGameObjectWithTag("Player");
        protaHP = prota.GetComponent<GestorSaludProta>();
    }
    public void newCheckpoint(Transform spawnPoint){
        currentPoint = spawnPoint;
    }

    public void returnToCheckpoint(Transform objectToTeleport){
        
        prota.transform.position = currentPoint.position;
        protaHP.RemoveHealth(10);
    }
}
