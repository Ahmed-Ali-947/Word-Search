using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SelectionManager selectionManager;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private WisdomManager wisdomManager;
    [SerializeField] private int coinsPerLevel = 10;
    [SerializeField] private int wisdomPerLevel = 15;
    private void OnEnable()
    {
        if (levelManager != null)
        {
            levelManager.LevelCompleted += OnLevelCompleted;
        }
    }

    private void OnDisable()
    {
        if (levelManager != null)
        {
            levelManager.LevelCompleted -= OnLevelCompleted;
        }
    }

    private void OnLevelCompleted()
    {
        selectionManager.SetInputEnabled(false);

        gridManager.SetLettersVisible(false);

        uiManager.ShowLevelComplete();

        coinManager.AddCoins(coinsPerLevel);
        wisdomManager.ChangeWisdom(wisdomPerLevel);
    }

    public void NextLevel()
    {
        GameProgress.CurrentLevel++;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }
}