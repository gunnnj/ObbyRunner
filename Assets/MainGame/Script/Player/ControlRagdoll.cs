using UnityEngine;

public class ControlRagdoll : MonoBehaviour
{
    public Rigidbody[] rigidbodies;

    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        EnableRagdoll();
    }
    [ContextMenu("Dis")]
    public void DisableRagdoll(){
        foreach(var item in rigidbodies){
            item.mass = 0;
            item.angularDamping = 0;
            item.useGravity = false;
            item.isKinematic = true;
        }
    }
    [ContextMenu("Enable")]
    public void EnableRagdoll(){
        foreach(var item in rigidbodies){
            item.useGravity = false;
            item.isKinematic = false;
        }
    }
}
