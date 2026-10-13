using UnityEngine;
using UnityEngine.SceneManagement;
public class WisdomManager : MonoBehaviour
{
    public void ChangeWisdom(int amount)
    {
        int currentWisdom = PlayerPrefs.GetInt(GameProgress.Wisdom, 0);
        currentWisdom += amount;
        PlayerPrefs.SetInt(GameProgress.Wisdom, currentWisdom);
        PlayerPrefs.Save();
    }
}