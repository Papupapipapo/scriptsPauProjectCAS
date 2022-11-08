using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestorNivel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI gemText;
    [SerializeField]
    Slider sliderStamina;
    [SerializeField]
    Slider sliderHealth;
    Image sliderImageStamina;
    Image sliderHealthStamina;

    [SerializeField]
    GameObject restartButton;

    [System.Serializable]
    public struct SliderColors{

        [Header("Health")]
        public Color Health;

        [Header("Stamina")]
        public Color Stamina ;
        public Color coolDownStamina;
        
    }
    public SliderColors slideColor;
    string defaultText = "0 -- 99";
    int gemsInLevelInt = 0;
    [SerializeField]
    int currentGems = 0;
    bool cooldownStaminabool = false;
    // Start is called before the first frame update
    void Start()
    {
        sliderImageStamina = sliderStamina.GetComponentsInChildren<Image>()[1];
        sliderHealthStamina = sliderHealth.GetComponentsInChildren<Image>()[1];
        GemsInLevel();
        Debug.Log(slideColor.Stamina);
        sliderHealthStamina.color = slideColor.Health;
        sliderImageStamina.color = slideColor.Stamina;
        UpdateStamina(100);
    }
    public void UpdateHealth(float currentHealth){
        sliderHealth.value = currentHealth;
    }
    public void UpdateStamina(float currentStamina){
        sliderStamina.value = currentStamina;
        if(currentStamina <= 2f){
            cooldownStaminabool = true;
            sliderImageStamina.color = slideColor.coolDownStamina;
        }
        if(cooldownStaminabool && currentStamina >= 30f){
            cooldownStaminabool = false;
             sliderImageStamina.color = slideColor.Stamina;
        }
    }
    void GemsInLevel(){
        GameObject [] gems = GameObject.FindGameObjectsWithTag("Gem");
        gemsInLevelInt = gems.Length;
        UpdateTextGems();
    }

    public void UpdateTextGems(){
        gemText.text = $"{currentGems.ToString("00")}  --  {gemsInLevelInt.ToString("00")}";
    }
    public void UpdateMoreTextGems(){
        currentGems++;
        UpdateTextGems();
    }
    public void showReset(){
        Cursor.lockState = CursorLockMode.None;
        restartButton.SetActive(true);
    
    }
    public bool checkIfAllGems(){
        return gemsInLevelInt == currentGems;
    }
}
