using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovment : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    [SerializeField]
    bool isStatic;
    [SerializeField]
    public Transform[] Checkpoints;
    int currentCheckInt = 0;
    Transform  currentCheckpoint;
    
    protected Animator enemyAnimController;

    Vector3 PreviousFramePosition = Vector3.zero; // Or whatever your initial position is
    protected float Speed = 0f;
    // Start is called before the first frame update
    void Awake(){
        enemyAnimController = GetComponent<Animator>();
    }
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if(!isStatic){
            agent.SetDestination(Checkpoints[0].position);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(!isStatic){
            getCurrentSpeed();
            updateAnimation();
            if (agent.remainingDistance <= agent.stoppingDistance){
                changeTarget();
            }
        }
        
        
    }

    void changeTarget() {//Once it gets to destination appoint new one, restarting if it's the end
        currentCheckInt = (currentCheckInt < Checkpoints.Length -1 ) ?  currentCheckInt + 1 : 0;
        currentCheckpoint = Checkpoints[currentCheckInt];
        agent.SetDestination(currentCheckpoint.position);
    }
     void getCurrentSpeed(){ //For the MoveSpeed
        float movementPerFrame = Vector3.Distance (PreviousFramePosition, transform.position) ;
        Speed = movementPerFrame / Time.deltaTime;
        PreviousFramePosition = transform.position;
    }
     protected void updateAnimation(){
        enemyAnimController.SetFloat("MoveSpeed", Speed);
     }
     public void triggerDeath(){
        enemyAnimController.SetTrigger("Dead");
     }
}
