using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryLine : MonoBehaviour
{
    [Header("Настройки траектории")]
    public int resolution = 120;
    public float maxTime = 15f;
    public float timeStep = 0.07f;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Правильная 3D траектория по кинематическим формулам
    /// </summary>
    public void DrawTrajectory(Vector3 startPos, Vector3 initialVelocity)
    {
        lineRenderer.positionCount = 0;
        List<Vector3> points = new List<Vector3>();
        points.Add(startPos);

        Vector3 pos = startPos;
        Vector3 vel = initialVelocity;

        for (int i = 0; i < resolution; i++)
        {
            float t = i * timeStep;

            // Кинематическая формула в 3D
            Vector3 point = startPos + vel * t + 0.5f * Physics.gravity * t * t;

            points.Add(point);

            // Останавливаемся, если упали ниже земли
            if (point.y < 0.3f)
                break;
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public void Show() => lineRenderer.enabled = true;
    public void Hide() => lineRenderer.enabled = false;

    public Vector3[] GetTrajectoryPoints(Vector3 startPos, Vector3 initialVelocity)
    {
        DrawTrajectory(startPos, initialVelocity);
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);
        return points;
    }
}