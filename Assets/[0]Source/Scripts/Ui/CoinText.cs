using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    public Text coinText;

    private void Start()
    {
        if (!coinText)
            coinText = GetComponent<Text>();
    }

    private void Update()
    {
        coinText.text = CoinManager.coins.ToString();
    }
}