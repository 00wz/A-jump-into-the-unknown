using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
{
    public void LoadNextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        LoadLevelsScript.RefreshProgress(nextLevelIndex);
        SceneManager.LoadScene(nextLevelIndex);
    }
    public void RestartWithProgress()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex;
        LoadLevelsScript.RefreshProgress(nextLevelIndex + 1);
        SceneManager.LoadScene(nextLevelIndex);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
