using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GunCar : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform target;
    [SerializeField] Transform pool;
    [SerializeField] float fireForce = 700f;
    [SerializeField] float timeRate = 4f;
    [SerializeField] int timeDestroyBullet = 5;
    List<GameObject> poolingBullet = new List<GameObject>();
    Vector3 dir;
    

    void Start()
    {

        dir = (target.position-transform.position).normalized;
        StartCoroutine(FireRateTime());
    }

    public IEnumerator FireRateTime(){
        while(true){
            FireBullet();
            yield return new WaitForSeconds(timeRate);
        }
    }
    [ContextMenu("Fire")]
    public async void FireBullet(){
        GameObject bull;
        if(GetBulletPool()==null){
            bull = Instantiate(bullet,transform.position,Quaternion.identity, pool);

            Rigidbody rb = bull.GetComponent<Rigidbody>();
            rb.AddForce(dir*fireForce, ForceMode.Impulse);

            await Task.Delay(timeDestroyBullet*1000);
            bull.SetActive(false);
            poolingBullet.Add(bull);
        }
        else{
            bull = GetBulletPool();
            bull.transform.position = transform.position;
            bull.SetActive(true);
            Rigidbody rb = bull.GetComponent<Rigidbody>();
            rb.AddForce(dir*fireForce, ForceMode.Impulse);

            await Task.Delay(timeDestroyBullet*1000);
            bull.SetActive(false);
        }
        
    }

    public GameObject GetBulletPool(){
        foreach(var item in poolingBullet){
            if(!item.activeSelf) return item;
        }
        return null;
    }


    
}
