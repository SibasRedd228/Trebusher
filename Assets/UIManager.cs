using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("Textos de la Interfaz (TextMeshPro)")]
    public TextMeshProUGUI forceText;
    public TextMeshProUGUI angleText;
    public TextMeshProUGUI hitsText;
    public TextMeshProUGUI messageText;

    private int hitCount = 0;

    private void Start()
    {
        UpdateUI(38f, 35f);
        if (messageText != null) 
            messageText.text = "";
    }

    /// <summary>
    /// Actualiza los valores de fuerza y ángulo en la UI
    /// </summary>
    public void UpdateUI(float force, float angle)
    {
        if (forceText != null) forceText.text = $"FUERZA: {force:F0}";
        if (angleText != null) angleText.text = $"ÁNGULO: {angle:F0}°";
        if (hitsText != null) hitsText.text = $"IMPACTOS: {hitCount}";
    }

    /// <summary>
    /// Registra un impacto exitoso y verifica si se alcanzó la victoria
    /// </summary>
    public void RegisterHit()
    {
        hitCount++;
        
        if (hitsText != null)
            hitsText.text = $"IMPACTOS: {hitCount}";

        ShowMessage("✅ ¡IMPACTO EXITOSO!", Color.green, 2f);

        // === VICTORIA DESPUÉS DE 2 IMPACTOS ===
        if (hitCount >= 2)
        {
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
                gm.RegisterVictory();
        }
    }

    public void ShowMiss()
    {
        ShowMessage("❌ FALLASTE", Color.yellow, 1.8f);
    }

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