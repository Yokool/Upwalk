using System.Collections;
using UnityEngine;

public class ShopCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject shopItemsContainer;
    private ShopItemsContainer shopItemsContainerScript;

    private void OnEnable()
    {
        shopItemsContainerScript = shopItemsContainer.GetComponent<ShopItemsContainer>();
        SpawnShopItems();
    }


    private void SpawnShopItems()
    {
        shopItemsContainerScript.SpawnShopItems();
    }

}
