using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
public class DamagePlayerTrigger : MonoBehaviour, ITriggerCallback
{
    [SerializeField]
    private int damageAmount;

    public void HandleTrigger(TriggerEvent message)
    {
        
        if(message.trigerringObject.m_TileType == TileType.ALIVE)
        {

            // This is the easiest way to check if the triggering object is the player
            if(message.trigerringObject.GetComponent<PlayerScript>() == null)
            {
                return;
            }


            HealthComponent health;
            health = message.trigerringObject.gameObject.GetComponent<HealthComponent>();

            if (health != null)
            {
                health.Damage(damageAmount);
            }

        }

    }
}
