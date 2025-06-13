using UnityEngine;

public class Seesaw : MonoBehaviour
{
    public float rotationSpeed = 3f;
    public bool isRight = false;
    public bool useX = true;
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
            if(useX){
                if(other.contacts[0].point.x<transform.position.x){
                    isRight = true;
                }
                else{
                    isRight = false;
                }
            }else{
                if(other.contacts[0].point.z<transform.position.z){
                    isRight = true;
                }
                else{
                    isRight = false;
                }
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
