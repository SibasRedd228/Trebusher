using UnityEngine;

public class TrebuchetAimer : MonoBehaviour
{
    [Header("Ajuste de Ángulo")]
    [Tooltip("Ángulo mínimo (hacia abajo)")]
    public float minAngle = -10f;

    [Tooltip("Ángulo máximo (hacia arriba)")]
    public float maxAngle = 70f;

    [Tooltip("Velocidad de rotación del ángulo")]
    public float rotationSpeed = 50f;

    private float currentAngle = 35f;

    void Update()
    {
        float input = 0f;

        // Control con teclas W/S o Flechas Arriba/Abajo
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
            input = 1f;
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
            input = -1f;

        // Actualizamos el ángulo
        currentAngle -= input * rotationSpeed * Time.deltaTime;
        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);
    }

    /// <summary>
    /// Devuelve la dirección de lanzamiento según el ángulo actual
    /// </summary>
    public Vector3 GetLaunchDirection()
    {
        return Quaternion.Euler(currentAngle, 0, 0) * Vector3.forward;
    }

    /// <summary>
    /// (Opcional) Devuelve el ángulo actual para usarlo en UI
    /// </summary>
    public float GetCurrentAngle()
    {
        return currentAngle;
    }
}