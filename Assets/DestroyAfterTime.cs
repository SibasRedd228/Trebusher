using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [Header("Tiempo de vida de los escombros")]
    public float lifetime = 8f;           // Tiempo en segundos antes de destruir los fragmentos
    public float fadeOutTime = 2f;        // Tiempo para que se desvanezcan (efecto de fade)

    private float timer;
    private Renderer[] renderers;

    private void Start()
    {
        timer = lifetime;
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        // Destruir el objeto cuando se acabe el tiempo
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        // Efecto de desvanecimiento progresivo
        else if (timer <= fadeOutTime && renderers.Length > 0)
        {
            float alpha = timer / fadeOutTime;

            foreach (var rend in renderers)
            {
                if (rend.material.HasProperty("_Color"))
                {
                    Color color = rend.material.color;
                    color.a = alpha;
                    rend.material.color = color;
                }
            }
        }
    }
}