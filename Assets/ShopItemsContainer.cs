using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemsContainer : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabShopItem;

    public void SpawnShopItems()
    {
        foreach(ShopItem shopItem in ShopItemDatabase.ShopItems)
        {
            GameObject instantiatedShopItem = Instantiate(prefabShopItem);
            instantiatedShopItem.GetComponent<ShopItemUI>().SetFromShopItem(shopItem);
            instantiatedShopItem.transform.parent = transform;
        }
    }

}
