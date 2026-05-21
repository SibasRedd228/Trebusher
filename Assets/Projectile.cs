using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Настройки урона")]
    public float minDamage = 40f;      // минимальный урон
    public float maxDamage = 160f;     // максимальный урон при сильном ударе
    public float maxLifetime = 15f;
    public float velocityDamping = 0.92f;

    private Rigidbody rb;
    private TrebuchetController controller;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    public void Init(TrebuchetController ctrl)
    {
        controller = ctrl;
    }

    private void Start()
    {
        Invoke(nameof(ReturnToTrebuchet), maxLifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        float impactSpeed = rb.linearVelocity.magnitude;
        float finalDamage = CalculateDamage(impactSpeed);

        Debug.Log($"💥 Удар со скоростью {impactSpeed:F1} | Урон: {finalDamage:F0}");

        if (collision.gameObject.CompareTag("Target"))
        {
            Destructible dest = collision.gameObject.GetComponent<Destructible>();
            if (dest != null)
            {
                dest.TakeDamage(finalDamage);

                UIManager ui = FindObjectOfType<UIManager>();
                if (ui != null) ui.RegisterHit();
            }
        }
        else
        {
            UIManager ui = FindObjectOfType<UIManager>();
            if (ui != null) ui.ShowMiss();
        }

        // Затухание после удара
        if (rb != null)
        {
            rb.linearVelocity *= velocityDamping;
            rb.angularVelocity *= 0.85f;
        }
    }

    /// <summary>
    /// Расчёт урона по скорости удара
    /// </summary>
    private float CalculateDamage(float speed)
    {
        // Нормализуем скорость (примерно от 0 до 50-60)
        float normalized = Mathf.Clamp01(speed / 45f);
        return Mathf.Lerp(minDamage, maxDamage, normalized);
    }

    public void StopProjectile()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
        CancelInvoke();
    }

    private void ReturnToTrebuchet()
    {
        StopProjectile();
        if (controller != null)
            controller.ResetProjectile();
    }
}