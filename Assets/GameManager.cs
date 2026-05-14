using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Панель победы")]
    public GameObject victoryPanel;

    private bool gameEnded = false;

    public void RegisterVictory()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}