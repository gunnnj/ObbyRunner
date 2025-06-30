using System.Threading.Tasks;
using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    [SerializeField] public float timeDisactive = 6f;
    private Rigidbody rb;
    private Vector3 startPos;
    private Quaternion quaternion;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        quaternion = transform.rotation;
    }
    async void OnEnable()
    {
        await Task.Delay((int)(timeDisactive*1000));
        gameObject.SetActive(false);
    }
    public void ResetPosition(){
        transform.position = startPos;
        transform.rotation = quaternion;
    }
    public void AddForce(float force, Vector3 dir){
        rb.AddForce(dir*force,ForceMode.Impulse);
    }
}
