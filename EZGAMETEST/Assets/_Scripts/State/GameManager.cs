using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager
{
    public static float difficulty = 1f;

    public static void StartGame(float difficultyMultiplier)
    {
        difficulty = difficultyMultiplier;
        SceneManager.LoadScene("FightingScene");
    }
}
