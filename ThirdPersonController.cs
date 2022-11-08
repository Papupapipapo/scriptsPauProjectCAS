using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    //
    // Start is called before the first frame update
    [SerializeField]
    private float walkspeed;
    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private Transform cameraTransform;


    private float speed;

    private float orignalStepOffset;
    private CharacterController characterController;
    private Animator animationController;

    private float ySpeed;
    private float horizontalInput;
    private float verticalInput;

    private bool isJumping;
    private bool isGrounded;
    private bool isRunning;
    private bool isFalling;
    [SerializeField]
    private float jumpSpeed;
    private bool canJump;

    private int jumpsAvailable = 2;
    private int jumpsAvailableTotal;
    [SerializeField]
    private float jumpOffsetGrace; //para saltar aunque sea un poco mas tarde
    private float? lastGroundedTime;

   

    private FootSteps thisFeet;
    private AudioHandler playerSounds;
    private GestorStamina playerStamina;
    

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animationController = GetComponent<Animator>();
        thisFeet = GetComponent<FootSteps>();
        playerSounds = GetComponent<AudioHandler>();
        playerStamina = GetComponent<GestorStamina>();
        jumpsAvailableTotal = jumpsAvailable;
        orignalStepOffset = characterController.stepOffset;
        //pillar animator
        //pillar reproductor sonido
        //Gestor salud
        //Gestor Stamina
        //respawn
    }

    // Update is called once per frame
    void Update()
    {
        //GetInputs;
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");



       


        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(direction.magnitude) * speed; //Fixes error on controller
        Vector3 directionfix = direction;
        directionfix.Normalize();

        direction = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * direction;


        direction.Normalize();


        ySpeed += Physics.gravity.y * Time.deltaTime;


         if (playerStamina.canRun() && (direction.magnitude > 0) && !(isFalling) && Input.GetKey(KeyCode.LeftShift))
        {
                isRunning = true;
                speed = runSpeed;
                playerStamina.looseStamina();
            
        }
        else
        {
            isRunning = false;
            playerStamina.getBackStamina();
            speed = walkspeed;
        }

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time; //Gives grace time for jumping
            JumpReset();
        }
        else
        {
            isGrounded = false;
            characterController.stepOffset = 0;
            if ((isJumping && ySpeed < 0) || ySpeed < -3)
            {
                isFalling = true;
                thisFeet.enabled = false;
                animationController.SetBool("IsFalling", isFalling);
            }
        }

        if (canJump && Input.GetButtonDown("Jump"))
        {
            playerSounds.IdentifySound("Jump");
            thisFeet.enabled = false;
            Jump();
        }

        Vector3 velocity = direction * magnitude;
        velocity.y = ySpeed;


        characterController.Move(velocity * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720f * Time.deltaTime);
        }


        if (isRunning)
        {
            horizontalInput += 1 * directionfix.x;
            verticalInput += 1 * directionfix.z;
        }

        if (direction != Vector3.zero)
        {
            animationController.SetBool("IsMoving", true);
        }
        else
        {
            animationController.SetBool("IsMoving", false);
        }

        animationController.SetFloat("MovimientoX", horizontalInput);
        animationController.SetFloat("MovimientoY", verticalInput);
        animationCheckJump();

        //Pillar x y y en input
        //mover segun input
        //movimiento que afecte animator
        //
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void JumpReset()
    {
        characterController.stepOffset = orignalStepOffset;
        jumpsAvailableTotal = jumpsAvailable;
        isGrounded = true;
        ySpeed = -0.5f;
        canJump = true; //Control if has jumps
        isJumping = false;
        isFalling = false;
        thisFeet.enabled = true;
        animationController.SetBool("IsFalling", isFalling);
    }
    void Jump()
    {
        if (isJumping)
        { //Second Jump
            secondJump();
        }
        else
        { //First Jump
            if (Time.time - lastGroundedTime <= jumpOffsetGrace)
            { //Checks if player has fallen off platform and gives time to jump
                ySpeed = jumpSpeed;
            }
            else
            {
                jumpsAvailableTotal--; //Compensate for lost jump, for being too late
                secondJump();
            }
        }
        jumpsAvailableTotal--;
        isJumping = true; //Activates second jump if starting from ground
    }
    void secondJump()
    {
        if (jumpsAvailableTotal == 1)
        {
            canJump = false;
        }
        ySpeed = jumpSpeed * Mathf.Clamp(1f - ((jumpsAvailable - jumpsAvailableTotal) * 0.1f), 0.6f, 1f);
    }
    void animationCheckJump()
    {
        animationController.SetBool("IsJumping", isJumping);
        animationController.SetBool("IsGrounded", isGrounded);
    }
}
