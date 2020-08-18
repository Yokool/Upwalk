using UnityEngine;

public static class ShopNames
{

    private static Sprite testName;
    public static Sprite TestName => testName;

    public static void LoadShopNames()
    {
        testName = Resources.Load<Sprite>("Sprites/NameTest");
    }

}