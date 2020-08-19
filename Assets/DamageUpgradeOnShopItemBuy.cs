public class DamageUpgradeOnShopItemBuy : IOnShopItemBuy
{
    public void OnShopItemBuy()
    {
        PersistentFiles.PlayerAttackData.IncreasetAttackType();
        PersistentFiles.PlayerAttackData.Save();
    }
}

