using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryLine : MonoBehaviour
{
    [Header("Configuración de la Trayectoria")]
    [Tooltip("Cantidad de puntos en la línea (mayor = más suave)")]
    public int resolution = 120;

    [Tooltip("Tiempo máximo de simulación en segundos")]
    public float maxTime = 15f;

    [Tooltip("Paso de tiempo (más pequeño = más precisión)")]
    public float timeStep = 0.07f;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Dibuja la trayectoria usando fórmulas cinemáticas en 3D
    /// x = x0 + vx * t + 0.5 * ax * t²
    /// y = y0 + vy * t + 0.5 * ay * t²
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

            // Fórmula cinemática 3D
            Vector3 point = startPos + vel * t + 0.5f * Physics.gravity * t * t;

            points.Add(point);

            // Detener la línea si toca el suelo
            if (point.y < 0.3f)
                break;
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    /// <summary>
    /// Muestra la línea de trayectoria
    /// </summary>
    public void Show() => lineRenderer.enabled = true;

    /// <summary>
    /// Oculta la línea de trayectoria
    /// </summary>
    public void Hide() => lineRenderer.enabled = false;

    /// <summary>
    /// Devuelve los puntos de la trayectoria (usado por GhostTrajectory)
    /// </summary>
    public Vector3[] GetTrajectoryPoints(Vector3 startPos, Vector3 initialVelocity)
    {
        DrawTrajectory(startPos, initialVelocity);
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);
        return points;
    }
}