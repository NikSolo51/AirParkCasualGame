using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static int coins = 400;
    
    public void AddCoins(int money)
    {
        coins += money;
    }
}
