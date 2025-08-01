using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class Character : MonoBehaviour

{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private PlayerMovement playerMovement;
    
    [SerializeField] 
    public Animator Animator;
    private string currentAction;
    
    private Vector3 flymoving;
    
    public float interactionRayLength = 5;
    
    

    public LayerMask groundMask;
    
    public bool fly = false;
    
    bool isWaiting = false;
    
    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        playerInput.OnMouseClick += HandleMouseClick;
        playerInput.OnFly += HandleFlyClick;
    }
    private void HandleFlyClick()
    {
        fly = !fly;
    }
    void Update()
    {
        if (fly)
        {
            playerMovement.Fly(playerInput.MovementInput, playerInput.IsJumping, playerInput.RunningPressed);
        }
        else
        {
            if (playerMovement.IsGrounded && playerInput.IsJumping && isWaiting == false)
            {
                isWaiting = true;
                StopAllCoroutines();
                StartCoroutine(ResetWaiting());
            }
            if (playerMovement.IsGrounded)
            {
                if (playerInput.RunningPressed)
                {
                    Animator.Play("RobotArmature|Robot_Running");
                }
                else if(playerInput.MovementInput.x > 0 || playerInput.MovementInput.y > 0 || playerInput.MovementInput.z > 0)
                {
                    Animator.Play("RobotArmature|Robot_Walking");
                }
                else if (playerInput.MovementInput.x < 0 || playerInput.MovementInput.y < 0 ||
                         playerInput.MovementInput.z < 0)
                {
                    Animator.Play("RobotArmature|Robot_Walking_Bakwards");
                }
                else
                {
                    Animator.Play("RobotArmature|Robot_Idle");
                }
                playerMovement.Walk(playerInput.MovementInput, playerInput.RunningPressed);
                flymoving = playerInput.MovementInput;
            }
            else
            {
                playerMovement.Walk(flymoving + (playerInput.MovementInput * (float)0.5), playerInput.RunningPressed);
            }
        }

        if (playerInput.IsJumping)
        {
            Animator.Play("RobotArmature|Robot_Jump");
        }
        playerMovement.HandleGravity(playerInput.IsJumping);

        
    }

    private void PlayAnimation(string animation)
    {
        if (animation == currentAction)
            return;

        if (playerInput.RunningPressed && playerMovement.IsGrounded)
        {
            Animator.Play("RobotArmature|Robot_Running");
        }
        
        Animator.Play("RobotArmature|Robot_Jump");
    }
    IEnumerator ResetWaiting()
    {
        yield return new WaitForSeconds(0.1f);
        isWaiting = false;
    }
    
    private void HandleMouseClick()
    {
        
    }
}
