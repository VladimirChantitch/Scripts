using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAndAnimation : MonoBehaviour
{   
    private Rigidbody player_rb;
    private Vector3 moveDirection;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private bool bIsReadyToJump;
    [SerializeField] private Transform orientation;
    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatIsHiddenGround;
    [SerializeField] LayerMask whatIsDestroyableMirror;
    [SerializeField] bool bIsGrounded;
    [Header("audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footStep_01;
    [SerializeField] private AudioClip jumpClip;


    public void TickUpdate(){
        bIsGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround)
                            || Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsHiddenGround)
                            || Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsDestroyableMirror);
        if (bIsGrounded){
            player_rb.drag = groundDrag;
        }else{
            player_rb.drag = 0f;
        }

        SpeedControl();
    }

    void Start(){
        player_rb = GetComponent<Rigidbody>();
        player_rb.freezeRotation = true;
    }

    public void MovePlayer(Vector2 v2_Movement){
        moveDirection = orientation.forward * v2_Movement.y + orientation.right * v2_Movement.x;

        if (moveDirection.x != 0 || moveDirection.z != 0){
            if(!audioSource.isPlaying && bIsGrounded){
                audioSource.PlayOneShot(footStep_01[UnityEngine.Random.RandomRange(0, footStep_01.Length-1)]);
            }
        }

        if (bIsGrounded){
            player_rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }else if (!bIsGrounded){
            player_rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }
    private void SpeedControl(){
        Vector3 flatVel = new Vector3(
                            player_rb.velocity.x,
                            0f,
                            player_rb.velocity.z
                        );

        if (flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            player_rb.velocity = new Vector3(limitedVel.x, player_rb.velocity.y, limitedVel.z);
        }
    }

    public void Jump(){
        if (!bIsReadyToJump || !bIsGrounded){
            return;
        }

        audioSource.PlayOneShot(jumpClip);

        bIsReadyToJump = false;
        Invoke(nameof(ResetJump), jumpCooldown);

        player_rb.velocity = new Vector3(player_rb.velocity.x, 0f, player_rb.velocity.z);

        player_rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump(){
        bIsReadyToJump = true;
    }
}
