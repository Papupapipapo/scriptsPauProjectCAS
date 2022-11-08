using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkHead : MonoBehaviour
{
    GestorSaludEnemigo saludEnemigo;
    float coolDownHurt = 0;
    [SerializeField]
    float bonkUp;
    private void Start()
    {
        saludEnemigo = this.GetComponentInParent<GestorSaludEnemigo>();
    }
    private void Update()
    {
        if (Time.time - coolDownHurt >= 0.5f && saludEnemigo)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, 0.3f);
            foreach (Collider collider in colliderArray)
            {

                if (collider.TryGetComponent(out GestorSaludProta saludContacto))
                {
                    saludContacto.KnockBack(Vector3.up * 2, bonkUp);
                    saludEnemigo.RemoveHealth(10);
                    coolDownHurt = Time.time;
                }
            }
        }

    }

}

