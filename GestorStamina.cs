using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorStamina : MonoBehaviour
{
    float totalStamina = 100f; //Numero arbitrario enrealidad, lo que mas import son los cooldown, sirve para hacer que sea mas 100%

    [SerializeField]
    float totalCooldownBack = 7f;
    [SerializeField]
    float totalCooldownLoose = 5f; //Segundos que tardara
    float getBackFloat;
    float looseFloat;
    float currentStamina;
    bool onCoolDown = false;

    GestorNivel levelMaster;

    void Start()
    {
        levelMaster = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<GestorNivel>();
        currentStamina = totalStamina;
        getBackFloat = (totalStamina / totalCooldownBack) * 0.01f; //A que ritmo recupera (le decimos cuanto stamina total i en cuanto tiempo deberia recuperar, de alli se baja para el delta)
        looseFloat = (totalStamina / totalCooldownLoose) * 0.01f; //A que ritmo pierde
    }

    public void getBackStamina()
    {
        if (currentStamina != totalStamina)
        {
            float currentStaminaDelta = currentStamina / totalStamina;
            currentStaminaDelta += getBackFloat * Time.deltaTime;
            currentStamina = Mathf.Lerp(0, totalStamina, currentStaminaDelta);
            levelMaster.UpdateStamina(currentStamina);
        }
    }
    public void looseStamina()
    {
        if (currentStamina != 0)
        {
            float currentStaminaDelta = currentStamina / totalStamina;
            currentStaminaDelta -= looseFloat * Time.deltaTime;
            currentStamina = Mathf.Lerp(0, totalStamina, currentStaminaDelta);
            levelMaster.UpdateStamina(currentStamina);
        }

    }

    public bool canRun()
    {
        if (onCoolDown)
        {
            if(currentStamina >= 30f){
                onCoolDown = false;
                return true;
            }else{
                return false;
            }
        }
        else if (!onCoolDown && currentStamina <= 2f)
        {
            onCoolDown = true;
            return false;
        }
        else
        {
            return true;
        }
    }

}
