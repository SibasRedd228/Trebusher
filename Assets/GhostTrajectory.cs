using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GhostTrajectory : MonoBehaviour
{
    [Header("Настройки")]
    public Color ghostColor = new Color(1f, 0.3f, 0.3f, 0.6f); // красноватый полупрозрачный
    public float lineWidth = 0.08f;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = ghostColor;
        lineRenderer.endColor = ghostColor;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.enabled = false;
    }

    public void ShowTrajectory(Vector3[] points)
    {
        if (points == null || points.Length < 2) return;

        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
        lineRenderer.enabled = true;
    }

    public void Hide()
    {
        lineRenderer.enabled = false;
    }
}