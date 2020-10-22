using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    #region Singleton:Game

    public static Game Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] private Text[] allCoinsUiText = null;
    [SerializeField] private TMP_Text[] allCoinsUiTextTMP = null;
    

    public int Coins;

    private void Start()
    {
        UpdateSetAllCoinsUiText();
    }

    public void UseCoins(int amount)
    {
        Coins -= amount;
    }

    public bool HasEnoughCoins(int amount)
    {
        return (Coins >= amount);
    }

    public void UpdateSetAllCoinsUiText()
    {
        for (int i = 0; i < allCoinsUiText.Length; i++)
        {
            allCoinsUiText[i].text = Coins.ToString();
        }

        for (int i = 0; i < allCoinsUiTextTMP.Length; i++)
        {
            allCoinsUiTextTMP[i].SetText(Coins.ToString());
        }
    }
}
