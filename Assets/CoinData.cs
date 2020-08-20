public class CoinData : PersistentFile
{

    public int Coins
    {
        get;
        set;
    } = 0;

    public CoinData()
    {

    }

    public CoinData(string filePath) : base(filePath)
    {
        Load();
        Save();
    }

    public override void PopulateFields(object deserializedObject)
    {
        CoinData coinData = deserializedObject as CoinData;
        this.Coins = coinData.Coins;
    }


    public void AssignFromSessionData(GameSessionData sessionData)
    {
        AddCoins(sessionData.Coins);
        Save();
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
    }

    public void RemoveCoins(int amount)
    {
        Coins -= amount;
    }

    public void SetCoins(int value)
    {
        Coins = value;
    }

}