using UnityEngine;
using UnityEngine.SceneManagement;

public class GameNavigation : MonoBehaviour
{
    [Header("Scene Names")]
    [Tooltip("The exact name of your Menu scene")]
    public string titleScreenName = "TitleScreen"; 

    public void GoToTitleScreen()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(titleScreenName);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}