using UnityEngine;

public class Seesaw : MonoBehaviour
{
    public float rotationSpeed = 3f;
    private bool isRight = false;
    private bool isStart = false;
    void Update()
    {
        if(isStart){
            if(!isRight){
                transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
            }
            else{
                transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
            }
        }
        
    }
    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            isStart = true;
            if(other.contacts[0].point.z<transform.position.z){
                isRight = true;
            }
            else{
                isRight = false;
            }
        }
    }
    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            isStart = false;
        }
    }
}
