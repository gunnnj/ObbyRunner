using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    Vector3 originPos;
    public bool isTarget = false;

    void Start()
    {
        originPos = transform.position;
    }
    void Update()
    {
        if(!isTarget){
            transform.position = Vector3.MoveTowards(transform.position,target.position,speed*Time.deltaTime);
            if(Vector3.Distance(transform.position,target.position)<0.1f){
                isTarget = true;
            }
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position,originPos,speed*Time.deltaTime);
            if(Vector3.Distance(transform.position,originPos)<0.1f){
                isTarget = false;
            }
        }
        
    }

}
