using UnityEngine;

public class Slip : MonoBehaviour
{
    [SerializeField] float force;
    public bool isForward = false;
    private Rigidbody _rb;
    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            _rb = other.gameObject.GetComponent<Rigidbody>();
            if(!isForward){
                _rb.AddForce(other.transform.forward*force,ForceMode.Impulse);
            }
            else{
                _rb.AddForce(transform.forward*force,ForceMode.Impulse);
            }
        }
    }
}
