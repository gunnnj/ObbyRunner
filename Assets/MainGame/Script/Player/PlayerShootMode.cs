using System;
using UnityEngine;

public class PlayerShootMode : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] Camera mainCam;
    private Vector3 dir;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        dir = new Vector3(dirX,0,dirY);

        if(dir.magnitude>0.1f){
            Move();
            
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
        Vector3 newPos = transform.position+dir;
        transform.position = Vector3.Lerp(transform.position,newPos,speed*Time.deltaTime);
    }
}
