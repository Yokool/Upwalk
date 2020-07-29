using UnityEngine;

public class OnPlayerDeath : MonoBehaviour, IOnDeath
{
    public void OnDeath()
    {
        GameLifetimeManager.INSTANCE.OnPlayerDeath();
    }
}
