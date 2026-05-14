using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Настройки камня")]
    public float damage = 100f;
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
        Debug.Log($"💥 Камень ударился в: {collision.gameObject.name} | Тег: {collision.gameObject.tag}");

        if (collision.gameObject.CompareTag("Target"))
        {
            Destructible dest = collision.gameObject.GetComponent<Destructible>();
            if (dest != null)
            {
                dest.TakeDamage(damage);

                // Обновляем UI
                UIManager ui = FindObjectOfType<UIManager>();
                if (ui != null)
                    ui.RegisterHit();
            }
            else
            {
                Debug.LogWarning("На цели нет Destructible!");
            }
        }
        else
        {
            // Попадание не в цель (земля, стена и т.д.)
            UIManager ui = FindObjectOfType<UIManager>();
            if (ui != null)
                ui.ShowMiss();
        }

        // Реалистичное затухание после отскока
        if (rb != null)
        {
            rb.linearVelocity *= velocityDamping;
            rb.angularVelocity *= 0.85f;
        }
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