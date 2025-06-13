using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class BotMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    [SerializeField] Checkpoint starCheckPoint;
    public AnimationCurve m_Curve = new AnimationCurve();
    public float jumpDuration = 1.0f;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
        agent.autoTraverseOffMeshLink = false;
    }
    async void Start()
    {
        await Task.Delay(200);
        Debug.Log(starCheckPoint.CountPoint());
        target = starCheckPoint.RandomPoint();
        agent.SetDestination(target.position);
    }
    void Update()
    {
        // Kiểm tra xem agent có đang ở trên một OffMeshLink và sẵn sàng di chuyển qua nó
        if (agent.isOnOffMeshLink)
        {
            if (!agent.isStopped && !IsInvoking("JumpCoroutineHandler")) 
            {
                // Bắt đầu xử lý cú nhảy thông qua coroutine
                StartCoroutine(JumpCoroutineHandler());
            }
        }
        else
        {
            if (agent.isStopped)
            {
                agent.isStopped = false;
            }
            if (target != null && agent.remainingDistance < 0.5f && !agent.pathPending)
            {
                // agent.SetDestination(target1.position);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint")){
            Debug.Log("Check");
            int idCheck = other.GetComponent<Checkpoint>().IDcheckpoint;
            target = ListCheckpoint.Instance.GetPointNextCheckpoint(idCheck);
            if(target!=null) agent.SetDestination(target.position);
        }
    }

    IEnumerator JumpCoroutineHandler()
    {

        agent.isStopped = true; 
        
        // Gọi coroutine Curve của bạn
        yield return StartCoroutine(Curve(agent, jumpDuration));

        agent.CompleteOffMeshLink();

        agent.isStopped = false;

    }
    IEnumerator Curve(NavMeshAgent agent, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;      
        float normalizedTime = 0.0f;

        while (normalizedTime < 1.0f)
        {
            // Tính toán vị trí theo đường thẳng (Lerp)
            Vector3 currentLerpPos = Vector3.Lerp(startPos, endPos, normalizedTime);
            
            float yOffset = m_Curve.Evaluate(normalizedTime) * 3.0f; 

            // Áp dụng vị trí đã tính toán cho agent
            agent.transform.position = currentLerpPos + yOffset * Vector3.up;
            
            normalizedTime += Time.deltaTime / duration;
            yield return null; // Chờ một frame
        }

        // Đảm bảo agent kết thúc đúng ở vị trí cuối cùng của link
        agent.transform.position = endPos; 
    }
}
