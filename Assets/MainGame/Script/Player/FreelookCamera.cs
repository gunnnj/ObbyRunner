using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FreelookCamera : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    Image imgCam;
    [SerializeField] CinemachineFreeLook camFreelook;
    private Vector2 lastTouchPosition;
    string strMouseX = "Mouse X", strMouseY = "Mouse Y";

    // Tăng giảm giá trị này để điều chỉnh độ nhạy của camera
    [SerializeField] float touchSensitivityX = 0.1f; 
    [SerializeField] float touchSensitivityY = 0.1f;
    void Start()
    {
        imgCam = GetComponent<Image>();

        //mobile
        camFreelook.m_XAxis.m_InputAxisName = null;
        camFreelook.m_YAxis.m_InputAxisName = null;
    }
    public void OnDrag(PointerEventData eventData)
    {
        // if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //     imgCam.rectTransform,
        //     eventData.position,
        //     eventData.enterEventCamera,
        //     out Vector2 posOut
        // )){
        //     camFreelook.m_XAxis.m_InputAxisName = strMouseX;
        //     camFreelook.m_YAxis.m_InputAxisName = strMouseY;
        // }
        //mobile
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imgCam.rectTransform,
            eventData.position,
            eventData.enterEventCamera,
            out Vector2 posOut
        ))
        {
            Vector2 currentTouchPosition = eventData.position;
            Vector2 deltaTouch = currentTouchPosition - lastTouchPosition;

            // Áp dụng sự thay đổi vào InputAxisValue
            // Lưu ý: Giá trị này cần được điều chỉnh bằng touchSensitivity
            camFreelook.m_XAxis.m_InputAxisValue = deltaTouch.x * touchSensitivityX;
            camFreelook.m_YAxis.m_InputAxisValue = deltaTouch.y * touchSensitivityY;

            lastTouchPosition = currentTouchPosition; // Cập nhật vị trí chạm cuối cùng
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // OnDrag(eventData);
        //mobile
        lastTouchPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // camFreelook.m_XAxis.m_InputAxisName = null;
        // camFreelook.m_YAxis.m_InputAxisName = null;
        camFreelook.m_XAxis.m_InputAxisValue = 0;
        camFreelook.m_YAxis.m_InputAxisValue = 0;
    }
}
