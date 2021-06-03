using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    public int price;
    public int id;
    private bool purchased;

    public void BuySkin()
    {
        if (purchased)
            return;

        if (CoinManager.coins - price > 0)
        {
            Debug.Log("hello");
            CoinManager.coins -= price;
            purchased = true;
        }
        else
        {
            Debug.Log("notHelo");
        }
    }

    public void ChangeSkin()
    {
        if(purchased)
        SkinChanger.mainId = id;
    }
}