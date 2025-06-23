using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShootMode : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject gun;
    [SerializeField] float speed = 4f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float rayDistance = 20f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float checkDistance = 0.1f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform bulletPool;
    [SerializeField] Transform pointShoot;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int initialPoolSize = 16;
    private ShooterModeUI shooterModeUI;
    private Rigidbody rb;
    private Vector3 dir;
    private Vector3 lastCalculatedPoint;
    private GameObject bulletShoot;
    private List<GameObject> poolerBullet = new List<GameObject>();

    void Awake()
    {
        InitializeBulletPool();
    }
    void Start()
    {
        shooterModeUI = FindFirstObjectByType<ShooterModeUI>();
        rb = GetComponent<Rigidbody>();
        shooterModeUI.onJump = ()=> Jump();
        shooterModeUI.onShoot = ()=> Shoot();

    }

    void Update()
    {
        Rotate();
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        dir = new Vector3(dirX,0,dirY);

        if(dir.magnitude>0.1f){
            Move();
        }
        // if(Input.GetMouseButtonDown(0)){
        //     Shoot();
        // }
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }

    }
    private void Jump()
    {
        if(!IsGrounded()) return;
        rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
        Debug.Log("Jump");
    }
    private void InitializeBulletPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPool);
            bullet.SetActive(false);
            poolerBullet.Add(bullet); 

        }
    }

    private void Shoot()
    {
        lastCalculatedPoint = GetRayEndPoint();

        if (lastCalculatedPoint != Vector3.zero) 
        {
            Debug.DrawLine(mainCam.transform.position, lastCalculatedPoint, Color.yellow, 1f);
        }

        
        //Rotation gun
        Vector3 dirGun = (lastCalculatedPoint - pointShoot.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(dirGun);
        Vector3 eulerAngles = targetRotation.eulerAngles;
        eulerAngles.y = gun.transform.eulerAngles.y; 
        gun.transform.rotation = Quaternion.Euler(eulerAngles);




        Debug.DrawLine(pointShoot.position, lastCalculatedPoint, Color.red, 1f);

        bulletShoot = GetBullet();
        bulletShoot.GetComponent<Bullet>().SetInfo(pointShoot.position,lastCalculatedPoint);
        bulletShoot.SetActive(true);

    }


    public GameObject GetBullet(){
        foreach(var item in poolerBullet){
            if(!item.activeSelf) return item;
        }
        GameObject bullet = Instantiate(bulletPrefab, bulletPool);
        bullet.SetActive(false);
        poolerBullet.Add(bullet);
        return bullet;
    }

    public Vector3 GetRayEndPoint(){
        Vector3 rayOrigin = mainCam.transform.position;
        Vector3 rayDirection = mainCam.transform.forward;

        Ray ray = new Ray(rayOrigin, rayDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            return hit.point;
        }
        else
        {
            return rayOrigin + rayDirection * rayDistance;
        }
    }

    private void Rotate()
    {
        Vector3 dirLook = mainCam.transform.forward;
        dirLook.y = 0;
        Vector3 rotate = Vector3.RotateTowards(transform.forward,dirLook,rotateSpeed*Time.deltaTime,0f);
        transform.rotation = Quaternion.LookRotation(rotate);
    }

    private void Move()
    {
        Vector3 inputDirection = dir.normalized;
        Vector3 moveDirection = transform.TransformDirection(inputDirection);
        transform.position += moveDirection * speed * Time.deltaTime;
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, checkDistance, groundLayer);
        // Vector3 checkPosition = transform.position - new Vector3(0, checkDistance, 0);

        // return Physics.CheckSphere(checkPosition, checkDistance, groundLayer);
    }
}
