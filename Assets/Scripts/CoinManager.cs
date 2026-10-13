using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int Coins { get; private set; }

    public event Action<int> CoinsChanged;

    private void Awake()
    {
        Coins = PlayerPrefs.GetInt(GameProgress.Coins, 0);

        CoinsChanged?.Invoke(Coins);
    }

    public void AddCoins(int amount)
    {
        Coins += amount;

        PlayerPrefs.SetInt(GameProgress.Coins, Coins);
        PlayerPrefs.Save();

        CoinsChanged?.Invoke(Coins);
    }

    public bool SpendCoins(int amount)
    {
        if (Coins < amount)
            return false;

        Coins -= amount;

        PlayerPrefs.SetInt(GameProgress.Coins, Coins);
        PlayerPrefs.Save();

        CoinsChanged?.Invoke(Coins);

        return true;
    }

    public void ResetCoins()
    {
        Coins = 0;

        PlayerPrefs.SetInt(GameProgress.Coins, Coins);
        PlayerPrefs.Save();

        CoinsChanged?.Invoke(Coins);
    }
}