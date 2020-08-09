using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Moveable))]
public class GridAttack : MonoBehaviour, IFailedToMoveToCallback
{
    [SerializeField]
    private int damageAmount;

    private IOnSuccessfulHitCallback successfulHitCallback;



    private void OnEnable()
    {
        successfulHitCallback = GetComponent<IOnSuccessfulHitCallback>();
    }

    public void FailedToMoveTo(int X, int Y, GridObject thisObject)
    {
        GridObject[] objectsAt = GameGrid.INSTANCE.ObjectsAt(X, Y);

        foreach(GridObject objectAt in objectsAt)
        {
            HealthComponent health = objectAt.GetComponent<HealthComponent>();
            if(health == null)
            {
                continue;
            }

            if(successfulHitCallback != null)
            {
                successfulHitCallback.OnSuccessfulHit();
            }

            
            health.Damage(damageAmount);

            // Plays the animation, although for every enemy we hit, so there might be multiple animations playing at the same place
            Vector3 attackAnimationPosition = objectAt.transform.position;
            GameObject obj = Instantiate(GameAnimation.attackAnimationPrefab, attackAnimationPosition, Quaternion.Euler(Vector3.zero));
            obj.GetComponent<SpriteRenderer>().sortingOrder = 15; // Make sure we render it ontop of everything
        }

    }

}