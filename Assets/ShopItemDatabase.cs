using System.Collections.Generic;
using UnityEngine;

public static class ShopItemDatabase
{

    private static List<ShopItem> shopItems;
    public static List<ShopItem> ShopItems => shopItems;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void _Initialize()
    {
        ShopNames.LoadShopNames();
        shopItems = new List<ShopItem>()
        {
            new ShopItem(200, ShopNames.TestName),
            new ShopItem(6500, ShopNames.TestName),
            new ShopItem(11111, ShopNames.TestName)
        };

    }

}
