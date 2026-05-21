using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Header("Salud")]
    public float health = 100f;

    [Header("Destrucción")]
    public GameObject destroyedVersion;     // Prefab de la versión destruida

    private void Start()
    {
        // Aseguramos que la salud no empiece en negativo
        if (health <= 0) 
            health = 100f;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} recibió {damage} de daño. Salud restante: {health}");

        if (health <= 0)
            Break();
    }

    private void Break()
    {
        Debug.Log($"{gameObject.name} → DESTRUIDO!");

        // Instanciamos la versión destruida (escombros)
        if (destroyedVersion != null)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
        }

        // Destruimos el objeto actual
        Destroy(gameObject);

        // === VICTORIA ===
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.RegisterVictory();
        }
    }
}