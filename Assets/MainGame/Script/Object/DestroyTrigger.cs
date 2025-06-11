using System.Threading.Tasks;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    [SerializeField] int timeDestroy = 2;
    async void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            await Task.Delay(timeDestroy*1000);
            gameObject.SetActive(false);
        }
    }
}
