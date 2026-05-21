using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Configuración de Cámara - Vista Lateral (Angry Birds Style)")]
    public Transform target;

    [Tooltip("Расстояние сбоку (влево/вправо). Изменяй для инверсии вида")]
    public float sideOffset = -15f;        // ← отрицательное = с другой стороны

    [Tooltip("Высота камеры")]
    public float heightOffset = 9f;

    [Tooltip("Отдаление назад")]
    public float backOffset = 8f;

    public float smoothSpeed = 7f;
    public float rotationSpeed = 5f;

    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Rigidbody rb = target.GetComponent<Rigidbody>();
        Vector3 velocity = rb.linearVelocity;

        Vector3 desiredPosition;

        if (velocity.magnitude > 2f)
        {
            // Основная логика: камера строго сбоку + немного сзади
            Vector3 sideDir = Vector3.Cross(Vector3.up, velocity.normalized).normalized;

            desiredPosition = target.position 
                            + sideDir * sideOffset           // Сбоку (меняй знак для инверсии)
                            + Vector3.up * heightOffset      // Высота
                            - velocity.normalized * backOffset; // Немного сзади
        }
        else
        {
            // Когда шар почти остановился — комфортный боковой вид
            desiredPosition = target.position + new Vector3(sideOffset * 0.8f, heightOffset, backOffset * 0.6f);
        }

        // Плавное движение
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, 
                                               ref currentVelocity, 1f / smoothSpeed);

        // Всегда смотрим точно на шар
        Vector3 lookDirection = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        currentVelocity = Vector3.zero;
    }

    public void ResetToDefault()
    {
        target = null;
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
        currentVelocity = Vector3.zero;
    }
}