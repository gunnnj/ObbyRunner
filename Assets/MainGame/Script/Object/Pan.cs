using System.Collections;
using UnityEngine;

public class Pan : MonoBehaviour
{
    private float jumpForce = 10f;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){

            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);

            Debug.Log("nảy bật");
        }
    }
    
}
