using System;

public class ClothesUpgradeOnShopItemBuy : IOnShopItemBuy
{
    public void OnShopItemBuy()
    {
        PlayerOutfitData outfitData = PersistentFiles.PlayerOutfitData;
        outfitData.OutfitType = ++outfitData.OutfitType;
    }
}

