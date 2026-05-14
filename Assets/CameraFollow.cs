using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Настройки камеры")]
    public Transform target;                    // снаряд
    public Vector3 offset = new Vector3(0, 8f, -18f);   // сзади и сверху
    public float smoothSpeed = 8f;
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

        // Камера всегда позади шара по направлению его движения
        Vector3 velocity = target.GetComponent<Rigidbody>().linearVelocity;

        Vector3 desiredPosition;

        if (velocity.magnitude > 2f)
        {
            // Летим сзади по направлению полёта
            Vector3 backOffset = -velocity.normalized * 18f;
            desiredPosition = target.position + backOffset + Vector3.up * 8f;
        }
        else
        {
            // Когда скорость маленькая — используем обычный offset
            desiredPosition = target.position + offset;
        }

        // Плавное движение
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, 1f / smoothSpeed);

        // Камера всегда смотрит на шар
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        currentVelocity = Vector3.zero; // сбрасываем инерцию
    }

    public void ResetToDefault()
    {
        target = null;
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
        currentVelocity = Vector3.zero;
    }
}