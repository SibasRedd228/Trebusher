using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [Header("Время жизни обломков")]
    public float lifetime = 8f;           // через сколько секунд исчезнуть
    public float fadeOutTime = 2f;        // время на затухание (опционально)

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

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        else if (timer <= fadeOutTime && renderers.Length > 0)
        {
            // Красивое затухание (становятся прозрачными)
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