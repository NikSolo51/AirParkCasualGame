using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
    public Text priceText;
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
            priceText.gameObject.SetActive(false);
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