using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("Textos de la Interfaz (TextMeshPro)")]
    public TextMeshProUGUI forceText;
    public TextMeshProUGUI angleText;
    public TextMeshProUGUI hitsText;
    public TextMeshProUGUI attemptsText;      // Contador de intentos
    public TextMeshProUGUI messageText;       // Mensajes centrales (éxito, fallo, victoria)

    private int hitCount = 0;                 // Impactos exitosos
    private int attemptCount = 0;             // Intentos realizados (máximo 3)

    private void Start()
    {
        UpdateUI(38f, 35f);
        if (messageText != null) 
            messageText.text = "";
        
        if (attemptsText != null)
            attemptsText.text = "INTENTOS: 0/3";
    }

    /// <summary>
    /// Actualiza la fuerza y el ángulo en la UI cada frame
    /// </summary>
    public void UpdateUI(float force, float angle)
    {
        if (forceText != null) forceText.text = $"FUERZA: {force:F0}";
        if (angleText != null) angleText.text = $"ÁNGULO: {angle:F0}°";
        if (hitsText != null) hitsText.text = $"IMPACTOS: {hitCount}";
    }

    /// <summary>
    /// Registra un nuevo intento (se llama al presionar Space)
    /// </summary>
    public void RegisterAttempt()
    {
        attemptCount++;
        if (attemptsText != null)
            attemptsText.text = $"INTENTOS: {attemptCount}/3";

        // Si se acabaron los 3 intentos y no se alcanzó la victoria
        if (attemptCount >= 3 && hitCount < 2)
        {
            ShowMessage("¡SE TERMINARON LOS INTENTOS!", Color.red, 4f);
        }
    }

    /// <summary>
    /// Registra un impacto exitoso contra el objetivo
    /// </summary>
    public void RegisterHit()
    {
        hitCount++;
        
        if (hitsText != null)
            hitsText.text = $"IMPACTOS: {hitCount}";

        ShowMessage("✓ ¡IMPACTO EXITOSO!", Color.green, 2f);

        // Victoria después de 2 impactos
        if (hitCount >= 2)
        {
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
                gm.RegisterVictory();
        }
    }

    public void ShowMiss()
    {
        ShowMessage("✕ FALLASTE", Color.yellow, 1.8f);
    }

    /// <summary>
    /// Muestra un mensaje temporal en el centro de la pantalla
    /// </summary>
    private void ShowMessage(string text, Color color, float duration)
    {
        if (messageText == null) return;

        messageText.text = text;
        messageText.color = color;

        StopAllCoroutines();
        StartCoroutine(ClearMessageAfter(duration));
    }

    private IEnumerator ClearMessageAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (messageText != null)
            messageText.text = "";
    }
}