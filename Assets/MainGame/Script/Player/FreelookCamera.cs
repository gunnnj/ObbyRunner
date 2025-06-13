using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FreelookCamera : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    Image imgCam;
    [SerializeField] CinemachineFreeLook camFreelook;
    private Vector2 lastTouchPosition;
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

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imgCam.rectTransform,
            eventData.position,
            eventData.enterEventCamera,
            out Vector2 posOut
        ))
        {
            Vector2 currentTouchPosition = eventData.position;
            Vector2 deltaTouch = currentTouchPosition - lastTouchPosition;

            camFreelook.m_XAxis.m_InputAxisValue = deltaTouch.x * touchSensitivityX;
            camFreelook.m_YAxis.m_InputAxisValue = deltaTouch.y * touchSensitivityY;

            lastTouchPosition = currentTouchPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastTouchPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        camFreelook.m_XAxis.m_InputAxisValue = 0;
        camFreelook.m_YAxis.m_InputAxisValue = 0;
    }
}
