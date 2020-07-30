using UnityEngine;

public class AddCoinsOnDeath : MonoBehaviour, IOnDeath
{
    [SerializeField]
    private int amount;

    public void OnDeath()
    {
        GameLifetimeManager.INSTANCE.AddCoins(amount);
    }
}