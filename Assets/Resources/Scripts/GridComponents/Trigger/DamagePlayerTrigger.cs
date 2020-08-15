using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
public class DamagePlayerTrigger : MonoBehaviour, ITriggerCallback
{
    [SerializeField]
    private int damageAmount;

    private GridObject thisGridObject;

    private void OnEnable()
    {
        thisGridObject = GetComponent<GridObject>();
    }

    public void HandleTrigger(TriggerEvent message)
    {
        GridObject triggeringObject = message.triggeringObject;
        if (triggeringObject.m_TileType == TileType.ALIVE)
        {

            // This is the easiest way to check if the triggering object is the player
            if(triggeringObject.GetComponent<PlayerScript>() == null)
            {
                return;
            }

            HealthComponent health;
            health = triggeringObject.gameObject.GetComponent<HealthComponent>();

            health.Damage(damageAmount);
            

        }

    }
}
