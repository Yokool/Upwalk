using System;

public class HealthUpgradeOnShopItemBuy : IOnShopItemBuy
{
    public void OnShopItemBuy()
    {
        PlayerHealthData playerHealthData = PersistentFiles.PlayerHealthData;
        playerHealthData.HealthAmount += 1;
    }
}

