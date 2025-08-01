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
    
    private Vector3 flymoving;
    
    public float interactionRayLength = 5;

    public LayerMask groundMask;
    
    public bool fly = false;
    
    public Animator animator;
    
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
        playerMovement.HandleGravity(playerInput.IsJumping);
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
                playerMovement.Walk(playerInput.MovementInput, playerInput.RunningPressed);
                flymoving = playerInput.MovementInput;
            }
            else
            {
                playerMovement.Walk(flymoving + (playerInput.MovementInput * (float)0.5), playerInput.RunningPressed);
            }
        }
        
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
