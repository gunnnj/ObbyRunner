using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float checkDistance = 0.1f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] float dashForce = 5f;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform cameraTransform;
    private Rigidbody rb;
    private VariableJoystick joystick;
    private PlayUI playUI;
    private CapsuleCollider capsuleCollider;
    private const string AnimRun = "isRun";
    private const string AnimJum = "isJump";
    private float _rotationVelocity;
    private float _verticalVelocity;
    Vector3 dir;
    bool canJump = true;
    int jumpCount = 0;
    int maxJump = 1;
    float originHeightCollider = 1.84f;
    

    public void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        playUI = FindFirstObjectByType<PlayUI>();
        playUI.onJump = ()=>Jump();
        joystick = playUI.joystick;
    }

    public void Update()
    {
    
        MoveAndRotate();
        ControlAnim();

    }
    //Add event Anim
    public void ScaleCollider(){
        capsuleCollider.height = 0.9f;
    }
    public void ScaleOriginCollider(){
        capsuleCollider.height = originHeightCollider;
    }

    private Action Jump()
    {
        if (canJump)
        {
            _verticalVelocity = jumpForce;
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
            Debug.Log("Jump");
            jumpCount++;
        }
        else{
            if(jumpCount<maxJump){
                jumpCount++;
                Debug.Log("Dash");
                //control dash
                rb.AddForce(transform.forward*dashForce,ForceMode.Impulse);
            }
        }
        
        return null;
    }
    public void ControlAnim(){
        if(dir.magnitude>0.1f){
            animator.SetBool(AnimRun,true);
        }
        else{
            animator.SetBool(AnimRun,false);
        }
        if (IsGrounded())
        {
            ScaleOriginCollider();
            jumpCount = 0;
            _verticalVelocity = 0f;
            animator.SetBool(AnimJum,false);
            canJump = true;
        }
        else{
            animator.SetBool(AnimJum,true);
            canJump = false;
        }
    }
    public void MoveAndRotate(){
        float dirX = joystick.Horizontal;
        float dirZ = joystick.Vertical;

        dir = new Vector3(dirX,0,dirZ);

        if(dir.magnitude>0.1f){
            // Move();
            MoveRotate();
        }
    }
    private void Move()
    {
        //old move
        Vector3 newPos = transform.position+dir;
        transform.position = Vector3.Lerp(transform.position,newPos,speed*Time.deltaTime);

    }
    public void MoveRotate(){
        //rotation
        float targetRotation = Mathf.Atan2(dir.x,dir.z)*Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity,
                    0.12f);

        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

        //move
        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
        
        Vector3 newPosition = transform.position + (targetDirection.normalized * speed *10* Time.deltaTime) +
                            new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f);
    
        // Vector3 rotate = Vector3.RotateTowards(transform.forward,dir,rotateSpeed*Time.deltaTime,0f);
        // transform.rotation = Quaternion.LookRotation(rotate);
    }
    private bool IsGrounded()
    {
        // return Physics.Raycast(transform.position, Vector3.down, checkDistance, groundLayer);
        Vector3 checkPosition = transform.position - new Vector3(0, checkDistance, 0);
    
        return Physics.CheckSphere(checkPosition, checkDistance, groundLayer);
    }
}
