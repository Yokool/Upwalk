using UnityEngine;

public class ShopItem
{

    private int cost;
    public int Cost => cost;

    private Sprite itemName;
    public Sprite ItemName => itemName;

    public ShopItem(int cost, Sprite itemName)
    {
        this.cost = cost;
        this.itemName = itemName;
    }


}
