using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float timeExplosion = 4f;
    private SphereCollider sphereCollider;
    private Coroutine coroutine;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
    }

    void Update()
    {
        if(gameObject.activeSelf){
            coroutine = StartCoroutine(ExpByTime());
            
        }
    }
    [ContextMenu("Boom")]
    public async void Explosion(){
        ManagerEffect.Instance.PlayEffect(ManagerEffect.Effect.bomb,transform.position);
        sphereCollider.enabled = true;
        await Task.Delay(100);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CanDestroy")){
            Destroy(other.gameObject);
        }
    }
    public IEnumerator ExpByTime(){
        yield return new WaitForSeconds(timeExplosion);
        Explosion();
        Debug.Log("aaaaa");
    }

}
