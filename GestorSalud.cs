using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorSalud : MonoBehaviour
{
    [SerializeField]
    protected AudioClip HurtSound;
    [SerializeField]
    protected AudioClip DeathSound;
    protected AudioSource sourceOfPain;
    protected Renderer objectMaterial;
    protected Color normalColor;
   [SerializeField]
    protected int totalHealth;
    
    protected int currentHealth;
    protected virtual void Start() {
        currentHealth = totalHealth;
        sourceOfPain = GetComponent<AudioSource>();
        objectMaterial = GetComponentInChildren<Renderer>();
        normalColor = objectMaterial.material.color;
    }
    void GetHealth(int givenHealth){
        currentHealth = (currentHealth + givenHealth > totalHealth)? totalHealth : currentHealth + givenHealth;
    }
    public virtual void RemoveHealth(int stolenHealth){
        if((currentHealth - stolenHealth < 0)){
            currentHealth = 0;
        }else{
            currentHealth =  currentHealth - stolenHealth;
            StartCoroutine(hurtBlink());
            
        }
         if(currentHealth == 0) {
            GameEnd(); }
            else playSound(HurtSound);  //If it doesn't die, play this sound
    }
    protected virtual void GameEnd(){
        playSound(DeathSound);
        StartCoroutine(WaitForSound());
    }
    protected IEnumerator hurtBlink(){
            objectMaterial.material.color = new Color(1,0,0);
            yield return new WaitForSeconds(0.3f);
            objectMaterial.material.color = normalColor;
    }
    protected void playSound(AudioClip playCurrSound){
        sourceOfPain.clip = playCurrSound;
        sourceOfPain.Play();
    }
    protected virtual IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(0.8f);
       //yield return new WaitUntil(() => sourceOfPain.isPlaying == false);
       // or yield return new WaitWhile(() => audiosource.isPlaying == true);
       Destroy(this.gameObject);
    }
    
}
