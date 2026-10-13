using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private TMP_Text coinText;

    private void OnEnable()
    {
        coinManager.CoinsChanged += UpdateCoins;

        UpdateCoins(coinManager.Coins);
    }

    private void OnDisable()
    {
        coinManager.CoinsChanged -= UpdateCoins;
    }

    private void UpdateCoins(int coins)
    {
        coinText.text = coins.ToString();
    }
}