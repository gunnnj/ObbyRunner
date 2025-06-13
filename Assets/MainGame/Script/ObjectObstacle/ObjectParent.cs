using UnityEngine;

public class ObjectParent : MonoBehaviour
{
    Transform oldParent;
    Transform player;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            player = other.transform;
            oldParent = player.parent;
            // Vector3 originalPosition = player.position;
            // Vector3 originalScale = player.localScale;

            player.SetParent(transform);

            // // Đặt lại vị trí và tỷ lệ
            // player.position = originalPosition;
            // player.localScale = originalScale;
        }
    }
    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            player = other.transform;
            player.SetParent(oldParent);
            // player.localScale = Vector3.one;
        }
    }
}
