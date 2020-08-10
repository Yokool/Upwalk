using UnityEngine;

[DisallowMultipleComponent]
public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private int health;
    public int Health => health;

    private IOnDeath[] onDeaths;
    private IOnHeal[] onHeals;
    private IOnDamage[] onDamages;


    private void OnEnable()
    {
        onDeaths = GetComponents<IOnDeath>();
        onHeals = GetComponents<IOnHeal>();
        onDamages = GetComponents<IOnDamage>();
    }

    public void Damage(int amount)
    {
        health -= amount;

        OnDamage();

        if(health <= 0)
        {
            health = 0;
            OnDeath();
        }

    }

    public void Heal(int amount)
    {
        health += amount;

        OnHeal();

    }

    public void OnDeath()
    {
        Debug.Log($"{gameObject} just died.");
        foreach (IOnDeath onDeath in onDeaths)
        {
            onDeath.OnDeath();
        }
    }

    public void OnHeal()
    {
        foreach (IOnHeal onHeal in onHeals)
        {
            onHeal.OnHeal();
        }
    }

    public void OnDamage()
    {
        foreach (IOnDamage onDamage in onDamages)
        {
            onDamage.OnDamage();
        }
    }

}
