using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class BotShooterMode : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] GameObject gun;
    [SerializeField] Transform bulletPool;
    [SerializeField] Transform pointShoot;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float navMeshSampleRadius = 1f;
    private Vector3 randomDirection;
    private Vector3 newPosition;
    private List<Bullet> listBullet = new List<Bullet>();

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewPos();
        GetBulletPooling();
    }
    void Update()
    {
        if(Vector3.Distance(transform.position,newPosition)<1.6f){
            SetNewPos();
        }
    }
    public void GetBulletPooling(){
        for(int i=0; i<bulletPool.childCount; i++){
            listBullet.Add(bulletPool.GetChild(i).GetComponent<Bullet>());
        }
    }
    // [ContextMenu("Random")]
    public void SetNewPos(){
        newPosition = RandomMove();
        agent.SetDestination(newPosition);
    }

    public Vector3 RandomMove(){
        NavMeshHit hit;
        Vector3 newPos ;

        do
        {
            randomDirection = Random.insideUnitSphere.normalized * RandomDistance();
            newPos = transform.position + randomDirection;
        } while (!NavMesh.SamplePosition(newPos, out hit, navMeshSampleRadius, NavMesh.AllAreas));

        return hit.position;
    }
    public float RandomDistance(){
        return Random.Range(5,15);
    }
    async void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            // Debug.Log("?");
            // agent.SetDestination(other.transform.position);
            // await Task.Delay(1000);
            // agent.speed = 0;


            // Shoot(other.transform.position);
        }
    }

    private void Shoot(Vector3 target)
    {
        foreach(var item in listBullet){
            if(!item.gameObject.activeSelf){
                item.SetInfo(pointShoot.position,target);
                item.gameObject.SetActive(true);
            }
        }
    }
}
