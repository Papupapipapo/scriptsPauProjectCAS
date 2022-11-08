using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    protected int damageEnemy = 10;

    protected virtual void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent(out GestorSaludProta saludContacto)){
            saludContacto.RemoveHealth(damageEnemy);
        }
    }
}
