using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Позиція, за якою слідує камера
    public float smoothSpeed = 0.125f; // Плавність руху камери
    public Vector3 offset; // Відстань між камерою і гравцем

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;  // Бажана позиція камери - позиція гравця + зміщення
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); // Плавний перехід між поточною позицією камери і бажаною позицією 
            transform.position = smoothedPosition;  // Встановлення нової позиції камери
        }
    }
}
