using UnityEngine;

public class Sink : MonoBehaviour
{
    [SerializeField] float decreaseRate = 1f;
    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            Vector3 newPosition = transform.position;
            newPosition.y -= decreaseRate * Time.deltaTime;
            transform.position = newPosition;
        }
    }
}
