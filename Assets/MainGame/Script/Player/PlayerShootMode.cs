using System.Collections.Generic;
using UnityEngine;


public class PlayerShootMode : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject gun;
    [SerializeField] float speed = 4f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float checkDistance = 0.1f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask layerShoot;
    [SerializeField] Transform bulletPool;
    [SerializeField] Transform pointShoot;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int initialPoolSize = 16;
    public TypeGun typeGun;
    private float rayDistance = 50f;
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
        PickGun(TypeGun.Light);
        // SetPowerByGun();

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

    public void PickGun(TypeGun type){
        typeGun = type;
        Debug.Log("Pick gun "+type.ToString());
        switch (typeGun){
            case TypeGun.Light:
                speed = 4.2f;
                rayDistance = 50f;
                break;
            case TypeGun.Medium:
                speed = 3.7f;
                rayDistance = 40f;
                break;
            case TypeGun.Weight:
                speed = 3.2f;
                rayDistance = 30f;
                break;
            default:
                break;
            
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
        bulletShoot.GetComponent<Bullet>().SetInfo(pointShoot.position,lastCalculatedPoint, typeGun);
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

        if (Physics.Raycast(ray, out hit, rayDistance,layerShoot))
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
