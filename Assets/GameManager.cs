using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Panel de Victoria")]
    public GameObject victoryPanel;     // Panel que se muestra al ganar

    private bool gameEnded = false;

    /// <summary>
    /// Se llama cuando el jugador gana (después de 2 impactos)
    /// </summary>
    public void RegisterVictory()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);
    }

    /// <summary>
    /// Reinicia la escena actual
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}