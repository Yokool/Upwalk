using UnityEngine;

public static class PersistentFiles
{

    private static PlayerAttackData playerAttackData;
    public static PlayerAttackData PlayerAttackData => playerAttackData;


    private static PlayerHealthData playerHealthData;
    public static PlayerHealthData PlayerHealthData => playerHealthData;


    private static PlayerOutfitData playerOutfitData;
    public static PlayerOutfitData PlayerOutfitData => playerOutfitData;


    private static CoinData playerCoinData;
    public static CoinData PlayerCoinData => playerCoinData;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void _Initialize()
    {
        playerAttackData = new PlayerAttackData("PlayerAttackData.upgrade", Turn.EASY);
        playerHealthData = new PlayerHealthData("PlayerHealthData.upgrade", 3);
        playerOutfitData = new PlayerOutfitData("PlayerOutfitData.upgrade", Turn.EASY);
        playerCoinData = new CoinData("PlayerCoinData.data");
    }


}
