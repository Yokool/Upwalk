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
    private IOnHealthComponentReady onHealthComponentReady;

    public void SetHealth(int value)
    {
        this.health = value;

        if(value > 0)
        {
            OnHeal();
        }
        else if(value < 0)
        {
            OnDamage();
        }

    }

    private void OnEnable()
    {
        onDeaths = GetComponents<IOnDeath>();
        onHeals = GetComponents<IOnHeal>();
        onDamages = GetComponents<IOnDamage>();

        onHealthComponentReady = GetComponent<IOnHealthComponentReady>();
        OnHealthComponentReady();
    }

    private void OnHealthComponentReady()
    {
        if(onHealthComponentReady != null)
        {
            onHealthComponentReady.OnHealthComponentReady();
        }
    }

    public void Damage(int amount)
    {
        health -= amount;

        // "It's fine, It's fine."
        if(health < 0)
        {
            health = 0;
        }

        OnDamage();

        if (health == 0)
        {
            OnDeath();
        }

    }

    public void Heal(int amount)
    {
        health += amount;

        OnHeal();

    }

    private void OnDeath()
    {
        foreach (IOnDeath onDeath in onDeaths)
        {
            onDeath.OnDeath();
        }
    }

    private void OnHeal()
    {
        foreach (IOnHeal onHeal in onHeals)
        {
            onHeal.OnHeal();
        }
    }

    private void OnDamage()
    {
        foreach (IOnDamage onDamage in onDamages)
        {
            onDamage.OnDamage();
        }
    }

}
