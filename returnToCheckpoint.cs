using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnToCheckpoint : MonoBehaviour
{
    private CheckpointReturn manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<CheckpointReturn>();
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.TryGetComponent<CharacterController>(out CharacterController playa))
        {
            playa.enabled = false;
            manager.returnToCheckpoint(coll.transform);
            playa.enabled = true;

        }

    }

}
