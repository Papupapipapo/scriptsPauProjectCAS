using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollector : MonoBehaviour
{
    AudioHandler humanSounds;
    [SerializeField]
    float interactRange = 2.5f;
    GestorNivel levelMaster;
    // Start is called before the first frame update
    void Start()
    {
        humanSounds = GetComponent<AudioHandler>();
        levelMaster = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<GestorNivel>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position,interactRange);
        foreach(Collider collider in colliderArray){
            if(collider.CompareTag("Gem")){
                humanSounds.IdentifySound("GemGet");
                levelMaster.UpdateMoreTextGems();
                //DARLE AL HUD
                Destroy(collider.gameObject);
            }
        }
    }
}
