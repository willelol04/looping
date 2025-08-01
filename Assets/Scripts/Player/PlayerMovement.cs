using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{

    [SerializeField]
    private CharacterController controller;


    
    [SerializeField]
    private float playerSpeed = 5.0f, playerRunSpeed = 8;

    [SerializeField]
    private float jumpHeight = 1.0f;

    [SerializeField]
    private float gravityValue = -9.81f;

    [SerializeField]
    private float flySpeed = 2;

    private bool Collided;
    private Vector3 playerVelocity;
    private Vector3 horizontalVelocity; // horizontal movement input velocity
    public bool collided;


    [Header("Grounded check parameters:")]
    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private float rayDistance = 1;
    [field: SerializeField]
    public bool IsGrounded { get; private set; }
    [SerializeField]
    private float jumpCooldownDuration = 0.5f; 
    private float jumpCooldownTimer = 0f;
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();

    }

    private Vector3 GetMovementDirection(Vector3 movementInput)
    {
        return transform.right * movementInput.x + transform.forward * movementInput.z;

    }
    public void Fly(Vector3 movementInput, bool ascendInput, bool descendInput)
    {

        Vector3 movementDirection = GetMovementDirection(movementInput);
        if (ascendInput)
        {

            movementDirection += Vector3.up * flySpeed;

        }
        else if (descendInput)
        {

            movementDirection -= Vector3.up * flySpeed;

        }
        controller.Move(movementDirection * playerSpeed * Time.deltaTime);

    }



    public void Walk(Vector3 movementInput, bool runningInput)
    {
        Vector3 movementDirection = GetMovementDirection(movementInput);
        

        float speed = runningInput ? playerRunSpeed : playerSpeed;

        if(!Collided)
            horizontalVelocity = movementDirection * speed;
    }



    public void HandleGravity(bool isJumping)
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        
        if (jumpCooldownTimer > 0f)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }
    
        if (isJumping && IsGrounded)
        {
            AddJumpForce();
        }
        if (isJumping && !IsGrounded || IsGrounded)
        {
            Collided = false;
        }
        ApplyGravityForce();

        Vector3 combinedVelocity = new Vector3(horizontalVelocity.x, playerVelocity.y, horizontalVelocity.z);
        
        CollisionFlags collisionFlags = controller.Move(combinedVelocity * Time.deltaTime);


        // Reset vertical velocity on ceiling collision
        if ((collisionFlags & CollisionFlags.Above) != 0 && playerVelocity.y > 0)
        {
            playerVelocity.y = 0f;
            // Debug.Log("Hit something above while jumping.");
        }

        if ((collisionFlags & CollisionFlags.Sides) != 0)
        {
            if (!IsGrounded && !IsNearWallOrGround())
            {
                // Fully airborne, hitting wall in air → stop horizontal velocity
                Collided = true;
                horizontalVelocity.x = 0f;
                horizontalVelocity.z = 0f;
                //Debug.Log("Hit a wall in mid-air, stopping momentum.");
            }
            else
            {
                // Grounded or near wall/ground → allow sliding or reduce speed smoothly
                horizontalVelocity.x *= 0.5f;
                horizontalVelocity.z *= 0.5f;
                // Debug.Log("Hit a wall near ground or wall, reducing speed.");
            }
        }
    }

    private bool IsNearWallOrGround()
    {
        // Check if near ground or a wall in a short radius
    
        float checkDistance = 0.5f; // tweak as needed

        // Check near ground
        bool nearGround = Physics.Raycast(transform.position, Vector3.down, checkDistance, groundMask);

        // Check near walls horizontally (in all 4 directions)
        bool nearWall = Physics.Raycast(transform.position, transform.right, checkDistance, groundMask)
                        || Physics.Raycast(transform.position, -transform.right, checkDistance, groundMask)
                        || Physics.Raycast(transform.position, transform.forward, checkDistance, groundMask)
                        || Physics.Raycast(transform.position, -transform.forward, checkDistance, groundMask);

        return nearGround || nearWall;
    }
    private void AddJumpForce()
    {
        //playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        if (jumpCooldownTimer <= 0f)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravityValue);
            jumpCooldownTimer = jumpCooldownDuration;
        }

    }
    
    private void ApplyGravityForce()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        playerVelocity.y = Mathf.Clamp(playerVelocity.y, gravityValue, 30f);
        Debug.Log(playerVelocity.y);
    }



    private void Update()
    {
        IsGrounded = IsGrounded = CheckIfGrounded();
    }

    private bool CheckIfGrounded()
    {
        Vector3 origin = transform.position;
        float checkRadius = 0.3f;
        Vector3[] offsets = new Vector3[5] {
            Vector3.zero,
            Vector3.forward * checkRadius,
            Vector3.back * checkRadius,
            Vector3.left * checkRadius,
            Vector3.right * checkRadius
        };

        foreach (var offset in offsets)
        {
            if (Physics.Raycast(origin + offset, Vector3.down, rayDistance, groundMask))
            {
                return true;
            }
        }

        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * rayDistance);
    }

}
