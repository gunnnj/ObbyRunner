using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FreelookCamera : MonoBehaviour
{
    public float sensitivity = 2.0f; // Độ nhạy
    private float rotationX = 0f;
    private float rotationY = 0f;

    public GameObject targetPanel; // Panel cụ thể cần kiểm tra

    void Update()
    {
        // Kiểm tra xem có chạm hay không
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Kiểm tra nếu chạm đang diễn ra
            if (touch.phase == TouchPhase.Moved)
            {
                // Kiểm tra xem chạm có nằm trên panel cụ thể không
                if (!IsTouchingSpecificPanel(touch))
                {
                    rotationX += touch.deltaPosition.x * sensitivity;
                    rotationY -= touch.deltaPosition.y * sensitivity;

                    // Giới hạn góc nhìn dọc
                    rotationY = Mathf.Clamp(rotationY, -30f, 30f);

                    // Áp dụng xoay cho camera
                    transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);
                }
            }
        }
    }

    // Kiểm tra xem có chạm vào panel cụ thể hay không
    private bool IsTouchingSpecificPanel(Touch touch)
    {
        // Tạo ray từ vị trí chạm
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = touch.position
        };

        // Danh sách các đối tượng bị chạm
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        // Kiểm tra từng kết quả xem có trùng với targetPanel không
        foreach (var result in results)
        {
            if (result.gameObject == targetPanel)
            {
                return true; // Đã chạm vào panel cụ thể
            }
        }

        return false; // Không chạm vào panel cụ thể
    }
}
