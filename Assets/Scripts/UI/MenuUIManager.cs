using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text wisdomText;
    private void Start()
    {
        UpdateLevelText(GameProgress.CurrentLevel);
        UpdateWisdomText(GameProgress.CurrentWisdom);
    }

    private void UpdateLevelText(int level)
    {
        levelText.text = $"Play Level {level + 1}";
    }

    private void UpdateWisdomText(int wisdom)
    {
        wisdomText.text = $"{wisdom}\nWisdom";
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}