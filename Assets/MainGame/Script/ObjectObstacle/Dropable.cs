using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dropable : MonoBehaviour
{
    [SerializeField] float timeReset = 2f;
    [SerializeField] float force = 500f;
    public GameObject[] objDrops;
    public bool isReverse = false;
    private List<Vector3> startPos = new List<Vector3>();
    private List<Quaternion> startRotation = new List<Quaternion>();

    void Start()
    {
        GetStartPos();
        ActiveObject(0);
        StartCoroutine(LoopDrop());
        
    }

    public void GetStartPos(){
        foreach(var item in objDrops){
            startPos.Add(item.transform.position);
            startRotation.Add(item.transform.rotation);
        }
    }
    public void ResetStartPos(int id){
        objDrops[id].transform.position = startPos[id];
        objDrops[id].transform.rotation = startRotation[id];
        
    }

    public void ActiveObject(int id){
        objDrops[id].SetActive(true);
    }

    // public void Drop(int id){
    //     if(isReverse){
    //         objDrops[id].AddForce(transform.forward*force,ForceMode.Impulse);
    //         return;
    //     }
    //     rigidbodies[id].AddForce(-transform.forward*force,ForceMode.Impulse);
        
    // }
    public IEnumerator LoopDrop(){
        int index = 1;
        yield return new WaitForSeconds(timeReset);
        while (true){
            if(!objDrops[index].activeSelf){
                ResetStartPos(index);
                ActiveObject(index);
                yield return new WaitForSeconds(timeReset);
                index++;
                if(index==objDrops.Count()){
                    index=0;
                }
            }
            else{
                index++;
                if(index==objDrops.Count()){
                    index=0;
                }
            }
            
        }
        
    }
}
