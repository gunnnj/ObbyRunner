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
        Vector3 newPos = dir + transform.position;
        transform.position = Vector3.Lerp(transform.position,newPos,speed*Time.deltaTime);

        // Vector3 forward = cameraTransform.forward;
        // Vector3 right = cameraTransform.right;

        // // Chỉ sử dụng hướng ngang và dọc
        // forward.y = 0;
        // right.y = 0;

        // // Chuẩn hóa vector
        // forward.Normalize();
        // right.Normalize();

        // // Tính toán hướng di chuyển dựa trên joystick
        // Vector3 moveDirection = forward * dir.z + right * dir.x;
        // Vector3 newPos = transform.position + moveDirection * speed * Time.deltaTime;

        // transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);

    }
    public void Rotate(){
        Vector3 rotate = Vector3.RotateTowards(transform.forward,dir,rotateSpeed*Time.deltaTime,0f);
        transform.rotation = Quaternion.LookRotation(rotate);
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, checkDistance, groundLayer);
    }
}
