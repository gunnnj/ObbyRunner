using System.Collections.Generic;
using UnityEngine;

public class Dropable : MonoBehaviour
{
    public List<Rigidbody> rigidbodies;

    void Start()
    {
        AddList();
    }
    public void AddList(){
        for(int i=0; i<transform.childCount; i++){
            Rigidbody rb = transform.GetChild(i).GetComponent<Rigidbody>();
            rb.useGravity = false;
            rigidbodies.Add(rb);
        }
    }
    [ContextMenu("Drop")]
    public void Drop(){
        foreach(var item in rigidbodies){
            item.useGravity = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            Drop();
        }
    }
}
