using UnityEngine;

public class UpdateHeartSpriteOnHealthChange : MonoBehaviour, IOnDamage, IOnHeal
{
    public void OnDamage()
    {
        HealthImageUpdater.INSTANCE.UpdateImage();
    }

    public void OnHeal()
    {
        HealthImageUpdater.INSTANCE.UpdateImage();
    }
}