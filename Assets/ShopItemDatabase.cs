using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

public static class ShopItemDatabase
{

    private const string dotFileExtension = ".shopItem";

    private static List<ShopItem> shopItems;
    public static List<ShopItem> ShopItems => shopItems;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void _Initialize()
    {
        ShopNames.LoadShopNames();

        // Init with default values
        shopItems = new List<ShopItem>()
        {
            new ShopItem("ClothesUpgrade", 200, ShopNames.TestName, new ClothesUpgradeOnShopItemBuy()),
            new ShopItem("HealthUpgrade", 6500, ShopNames.TestName, new HealthUpgradeOnShopItemBuy()),
            new ShopItem("DamageUpgrade", 11111, ShopNames.TestName, new DamageUpgradeOnShopItemBuy())
        };
        // Load the values if they exist
        LoadAllShopItems();


        // Save all files, creates new ShopItem files if they don't exist from default values
        SaveAllShopItems();

    }

    public static void LoadAllShopItems()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            ShopItem item = shopItems[i];

            string pathToItem = GetPathToShopItem(item);

            if (File.Exists(pathToItem))
            {
                shopItems[i] = LoadShopItem(pathToItem);
            }

        }
    }

    public static ShopItem LoadShopItem(string pathToShopItem)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ShopItem));

        return xmlSerializer.Deserialize(File.Open(pathToShopItem, FileMode.Open)) as ShopItem;

    }

    public static ShopItem LoadShopItem(ShopItem shopItem)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ShopItem));

        FileStream stream = File.Open(GetPathToShopItem(shopItem), FileMode.Open);

        ShopItem loadedItem = xmlSerializer.Deserialize(stream) as ShopItem;

        stream.Close();

        return loadedItem;

    }

    public static void SaveAllShopItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ShopItem));

        foreach (ShopItem item in shopItems)
        {
            // If it doesn't exist create it
            FileStream stream = File.Open(GetPathToShopItem(item), FileMode.Open);
            // Truncate it
            stream.SetLength(0);

            StreamWriter writer = new StreamWriter(stream);

            serializer.Serialize(writer, item);

            writer.Close();
            stream.Close();
        }

        
    }

    private static string GetPathToShopItem(ShopItem item)
    {
        return Application.persistentDataPath + item.FileItemName + dotFileExtension;
    }

}
