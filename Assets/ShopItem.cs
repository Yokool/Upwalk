using UnityEngine;

public class ShopItem
{

    private int cost;
    public int Cost => cost;

    private Sprite nameSprite;
    public Sprite NameSprite => nameSprite;

    private IOnShopItemBuy onShopItemBuy;
    public IOnShopItemBuy OnShopItemBuy => onShopItemBuy;

    private string fileItemName;
    public string FileItemName => fileItemName;

    private int upgradeAmount = 1;
    public int UpgradeAmount => upgradeAmount;

    private int maxUpgradeAmount = 1;
    public int MaxUpgradeAmount => maxUpgradeAmount;

    public ShopItem(string fileItemName, int maxUpgradeAmount, int cost, Sprite nameSprite, IOnShopItemBuy onShopItemBuy)
    {
        this.fileItemName = fileItemName;
        this.maxUpgradeAmount = maxUpgradeAmount;
        this.cost = cost;
        this.nameSprite = nameSprite;
        this.onShopItemBuy = onShopItemBuy;
    }

    public void OnBuy()
    {
        onShopItemBuy.OnShopItemBuy();
        ++upgradeAmount;
    }
}

