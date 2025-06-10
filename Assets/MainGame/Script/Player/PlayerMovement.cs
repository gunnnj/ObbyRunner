using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float checkDistance = 0.1f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform cameraTransform;
    private Rigidbody rb;
    private VariableJoystick joystick;
    private PlayUI playUI;
    Vector3 dir;
    bool canJump = true;
    private const string AnimRun = "isRun";
    private const string AnimJum = "isJump";

    

    public void Start()
    {
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

    private Action Jump()
    {
        if (canJump)
        {
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
            Debug.Log("Jump");
            
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
            Move();
            Rotate();
        }
    }
    private void Move()
    {
        // Vector3 forward = cameraTransform.forward;
        // Vector3 right = cameraTransform.right;
        // right.y = 0;
        // forward.y = 0;
        // forward.Normalize();
        // Vector3 newDir = (forward*dir.y + right*dir.x).normalized;

        // rb.linearVelocity = new Vector3(newDir.x*speed, rb.linearVelocity.y,newDir.z*speed);

        // if (newDir.magnitude > 0)
        // {
        //     Quaternion targetRotation = Quaternion.LookRotation(newDir);
        //     transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        //     transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        // }

        Vector3 newPos = transform.position+dir;
        transform.position = Vector3.Lerp(transform.position,newPos,speed*Time.deltaTime);



    }
    public void Rotate(){
        Vector3 rotate = Vector3.RotateTowards(transform.forward,dir,rotateSpeed*Time.deltaTime,0f);
        transform.rotation = Quaternion.LookRotation(rotate);
    }
    private bool IsGrounded()
    {
        // return Physics.Raycast(transform.position, Vector3.down, checkDistance, groundLayer);
        Vector3 checkPosition = transform.position - new Vector3(0, checkDistance, 0);
    
        // Kiểm tra xem có Collider nào trong phạm vi hình cầu
        return Physics.CheckSphere(checkPosition, checkDistance, groundLayer);
    }
}
