using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float rotateSpeed = 50f; 
    public float targetAngle = 60f; 
    private float currentAngle = 0f; 
    private bool rotatingToTarget = true; 

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float angleChange = rotateSpeed * Time.deltaTime;
        currentAngle += rotatingToTarget ? angleChange : -angleChange;

        currentAngle = Mathf.Clamp(currentAngle, -targetAngle, targetAngle);

        transform.rotation = Quaternion.Euler(0, 0, currentAngle);


        if (currentAngle >= targetAngle || currentAngle <= -targetAngle)
        {
            rotatingToTarget = !rotatingToTarget; // Đổi hướng xoay
        }
    }
}
