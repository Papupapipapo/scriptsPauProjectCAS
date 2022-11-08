using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    private CharacterController cc;
    [SerializeField]
    private AudioClip[] AudioClips;
    [SerializeField]
    private AudioSource feetSounds;
    [SerializeField]
    private float StepDistance;
    [SerializeField]
    private Transform head;
    private int currentGround = 1;
    private Vector3 lastRecordedDistance = new Vector3(1000,1000,0);

    float horizontalInput;
    float verticalInput;
     
    // Start is called before the first frame update
   
    private void OnEnable() {
        lastRecordedDistance = new Vector3(1000,1000,0);
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(head.position,Vector3.down);
        RaycastHit hit;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Physics.Raycast(ray, out hit, 100)){
            IdentifyGround(hit.transform.gameObject.tag);
        }
            

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        if(((this.transform.position - lastRecordedDistance).magnitude > StepDistance)&& direction.magnitude > 0){
            lastRecordedDistance = this.transform.position;
            feetSounds.clip = AudioClips[currentGround];
            feetSounds.volume = (Random.Range(0.3f, 0.6f));
            feetSounds.pitch = (Random.Range(0.8f, 1.0f));
            feetSounds.Play();
        }
    }
    void IdentifyGround(string tagGround){
        switch (tagGround)
        {
        case "Grass":
            currentGround = pickRandomFromRange(6,8);
            break;
        case "Stone":
            currentGround = currentGround = pickRandomFromRange(0,2);;
            break;
        case "Dirt":
            currentGround = pickRandomFromRange(3,5);
            break;
        default:
            currentGround = 0;
            break;
        }
    }
    int pickRandomFromRange(int rangeStart, int rangeEnd){
        return Random.Range(rangeStart,rangeEnd);
    }
}
