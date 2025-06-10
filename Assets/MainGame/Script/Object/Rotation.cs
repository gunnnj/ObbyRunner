using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public bool isUp = true;
    public bool isRight = false;
    public bool isForward = false;
    void Update()
    {
        if(isUp){
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            return;
        }
        if(isRight){
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
            return;
        }
        if(isForward){
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            return;
        }
        
    }
}
