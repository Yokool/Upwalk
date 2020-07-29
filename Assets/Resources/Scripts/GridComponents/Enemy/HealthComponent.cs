using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private int health;
    public int Health => health;

    private IOnDeath onDeath;
    private IOnHeal onHeal;
    private IOnDamage onDamage;


    private void OnEnable()
    {
        onDeath = GetComponent<IOnDeath>();
        onHeal = GetComponent<IOnHeal>();
        onDamage = GetComponent<IOnDamage>();
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
        if(onDeath != null)
        {
            onDeath.OnDeath();
        }
    }

    public void OnHeal()
    {
        if (onHeal != null)
        {
            onHeal.OnHeal();
        }
    }

    public void OnDamage()
    {
        if (onDamage != null)
        {
            onDamage.OnDamage();
        }
    }

}
