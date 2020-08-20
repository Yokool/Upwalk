public class DamageUpgradeOnShopItemBuy : IOnShopItemBuy
{
    public void OnShopItemBuy()
    {
        PersistentFiles.PlayerAttackData.IncreaseAttackType();
        PersistentFiles.PlayerAttackData.Save();
    }
}

