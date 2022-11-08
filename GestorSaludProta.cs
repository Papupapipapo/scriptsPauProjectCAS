using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorSaludProta : GestorSalud
{   
    private CharacterController characterController;
     float mass = 3.0F; // defines the character mass
    Vector3 impact = Vector3.zero;
    [SerializeField]
    private float KnockBackForce;
    GestorNivel levelMaster;
    protected override void Start() {
        base.Start();
        characterController = GetComponent<CharacterController>();
        levelMaster = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<GestorNivel>();
    }
    private void Update() {
        if (impact.magnitude > 0.2F) characterController.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }
    public void KnockBack(Vector3 dir, float force){
        AddImpact(dir, force);
    }

    void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;
    }
    public override void RemoveHealth(int stolenHealth){
        KnockBack(-(this.transform.forward) + Vector3.up , KnockBackForce);
        base.RemoveHealth(stolenHealth);
        levelMaster.UpdateHealth(currentHealth);
        
    }
    protected override void GameEnd(){
        base.GameEnd();
        levelMaster.showReset();
    }
    
}
