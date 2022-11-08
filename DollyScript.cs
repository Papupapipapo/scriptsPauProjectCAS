using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyScript : MonoBehaviour
{
    [SerializeField]
    ThirdPersonController personaje;
    public void enablePlayer(){
        personaje.enabled = true;
        Destroy(this.gameObject);
    }
}
