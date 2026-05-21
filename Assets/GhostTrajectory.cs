using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GhostTrajectory : MonoBehaviour
{
    [Header("Configuración de la Trayectoria Fantasma")]
    [Tooltip("Color de la trayectoria anterior (rojo semitransparente)")]
    public Color ghostColor = new Color(1f, 0.3f, 0.3f, 0.6f);

    [Tooltip("Grosor de la línea")]
    public float lineWidth = 0.08f;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
        // Configuramos el color y grosor
        lineRenderer.startColor = ghostColor;
        lineRenderer.endColor = ghostColor;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.enabled = false;
    }

    /// <summary>
    /// Muestra la trayectoria del lanzamiento anterior
    /// </summary>
    public void ShowTrajectory(Vector3[] points)
    {
        if (points == null || points.Length < 2) return;

        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
        lineRenderer.enabled = true;
    }

    /// <summary>
    /// Oculta la trayectoria fantasma
    /// </summary>
    public void Hide()
    {
        lineRenderer.enabled = false;
    }
}