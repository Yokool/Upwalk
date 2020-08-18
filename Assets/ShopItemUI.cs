using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
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

    private void OnEnable()
    {
        counters = new List<Image>();
        counters.Add(counter_1);
        counters.Add(counter_10);
        counters.Add(counter_100);
        counters.Add(counter_1000);
        counters.Add(counter_10000);
    }

    public void SetFromShopItem(ShopItem item)
    {
        itemName.sprite = item.ItemName;
        CoinCounter.AttachValuesToCounter(item.Cost, counters);
    }

}
