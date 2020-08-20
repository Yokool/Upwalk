using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{

    private ShopItem associatedItem;

    [SerializeField]
    private Image itemName;

    [SerializeField]
    private Image counter_1;
    [SerializeField]
    private Image counter_10;
    [SerializeField]
    private Image counter_100;
    [SerializeField]
    private Image counter_1000;
    [SerializeField]
    private Image counter_10000;

    private List<Image> counters;

    [SerializeField]
    private Button buyButton;

    private void Awake()
    {
        buyButton.onClick.AddListener(OnBuy);
        counters = new List<Image>();
        counters.Add(counter_1);
        counters.Add(counter_10);
        counters.Add(counter_100);
        counters.Add(counter_1000);
        counters.Add(counter_10000);
    }

    private bool EnoughMoney()
    {
        return PersistentFiles.PlayerCoinData.Coins >= associatedItem.Cost;
    }

    private void OnBuy()
    {
        if (EnoughMoney())
        {
            PersistentFiles.PlayerCoinData.RemoveCoins(associatedItem.Cost);
            PersistentFiles.PlayerCoinData.Save();
            associatedItem.OnBuy();
        }
        
    }

    public void SetFromShopItem(ShopItem item)
    {
        associatedItem = item;
        itemName.sprite = item.NameSprite;
        CoinCounter.AttachValuesToCounter(item.Cost, counters);
    }

}
