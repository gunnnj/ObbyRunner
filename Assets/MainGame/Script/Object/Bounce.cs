using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] float bounceForce = 5f;
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Bounce");
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 bounceDirection =  other.transform.position-transform.position;
            bounceDirection.y = 0;
            bounceDirection.Normalize();
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
