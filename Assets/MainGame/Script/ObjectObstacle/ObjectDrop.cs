using System.Threading.Tasks;
using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    private Dropable dropable;
    [SerializeField] public float timeDisactive = 6f;
    void Start()
    {
        dropable = GetComponentInParent<Dropable>();
    }
    async void OnEnable()
    {
        await Task.Delay((int)(timeDisactive*1000));
    }
}
