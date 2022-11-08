using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGround : Trap
{
    BoxCollider triggerCollider;
    bool currentStatus = false;
    
    void Awake(){
        triggerCollider = GetComponent<BoxCollider>();
    }

    public void triggerBoxCollider(){
        currentStatus = triggerCollider.enabled;
        if(currentStatus){
            //disabled
        }else{
            //disabled
        }
        triggerCollider.enabled = !currentStatus;
    }
}
