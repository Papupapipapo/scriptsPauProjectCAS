using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystemFlag : MonoBehaviour
{
    private  CheckpointReturn manager;
    [SerializeField]
    private Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<CheckpointReturn>();
    }

    void OnTriggerEnter(Collider coll){
        if(coll.CompareTag("Player")){
            manager.newCheckpoint(spawnPoint);
        }
        
    }
}
