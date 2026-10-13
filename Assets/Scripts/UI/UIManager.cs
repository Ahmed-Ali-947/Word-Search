using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject bonusWordsPanel;
    [SerializeField] private HintManager hintManager;
    [SerializeField] private TMPro.TMP_Text levelText;
    private void Start()
    {
        HideLevelComplete();
    }

    public void ShowLevelComplete()
    {
        levelCompletePanel.SetActive(true);
    }

    public void HideLevelComplete()
    {
        levelCompletePanel.SetActive(false);
    }
    public void UpdateLevelText(int level)
    {
        levelText.text = $"Level {level + 1}";
    }

    public void ShowBonusWordsPanel()
    {
        bonusWordsPanel.SetActive(true);
    }

    public void HideBonusWordsPanel()
    {
        bonusWordsPanel.SetActive(false);
    }

    public void GiveHint()
    {
        hintManager.ShowHint();
    }

    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}