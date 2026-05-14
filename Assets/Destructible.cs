using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Header("Здоровье")]
    public float health = 100f;

    [Header("Разрушение")]
    public GameObject destroyedVersion;

    private void Start()
    {
        if (health <= 0) health = 100f;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} получил {damage} урона. Осталось: {health}");

        if (health <= 0)
            Break();
    }

    private void Break()
    {
        Debug.Log($"{gameObject.name} → УНИЧТОЖЕН!");

        // Создаём разрушенную версию
        if (destroyedVersion != null)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
        }

        // Уничтожаем текущий объект
        Destroy(gameObject);

        // === ПОБЕДА ===
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.RegisterVictory();
        }
    }
}