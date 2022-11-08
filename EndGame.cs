using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    Animator chestAnimator;
    AudioSource auSauce;
    GestorNivel gestorNivelGema;
    [SerializeField]
    GameObject particleGem;
    [SerializeField]
    GameObject particleDuck;
    //check if 100% done
    //on trigger enter

    void Start(){
        chestAnimator = GetComponent<Animator>();
        auSauce = GetComponent<AudioSource>();
        gestorNivelGema = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<GestorNivel>();
    }
    void OnTriggerEnter(Collider collPlayer){
        if(collPlayer.TryGetComponent<GestorSaludProta>(out GestorSaludProta gestorSalud)){
            //play animation;
            //play sound;
            auSauce.enabled = true;
            chestAnimator.SetTrigger("OpenChest");
            if(chestAnimator.GetCurrentAnimatorStateInfo(0).IsName("Open") && 
                chestAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                   triggerOpen();
                }
        }
    }
    public void triggerOpen(){
        particleGem.SetActive(true);
         
                if(gestorNivelGema.checkIfAllGems()){
                   particleDuck.SetActive(true);
                }
    }
}