using UnityEngine;

[DisallowMultipleComponent]
public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private int health;
    public int Health => health;

    public void SetHealth(int health)
    {
        this.health = health;
    }

    private IOnDeath[] onDeaths;
    private IOnHeal[] onHeals;
    private IOnDamage[] onDamages;


    private void CustomInit()
    {
        onDeaths = GetComponents<IOnDeath>();
        onHeals = GetComponents<IOnHeal>();
        onDamages = GetComponents<IOnDamage>();
    }

    private void OnEnable()
    {
        CustomInit();
    }

    public void Damage(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(amount, 0, 5);

        OnDamage();

        if (health <= 0)
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
