using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(WallSpriteAdjuster))]
public class UpdateSpriteOnChange : MonoBehaviour, ISurroundingsChangedCallback 
{
    private WallSpriteAdjuster wallSpriteAdjuster;

    private void OnEnable()
    {
        wallSpriteAdjuster = GetComponent<WallSpriteAdjuster>();
    }

    public void SurroundingsChanged(GridObject observedObject)
    {
        wallSpriteAdjuster.UpdateSprite();

        // Since SurroundingsChanged gets called only on already existing
        // objects, we also have to have a way to update the newly added object and OnEnable
        // is a very unreliable way.
        WallSpriteAdjuster otherAdjuster = observedObject.GetComponent<WallSpriteAdjuster>();

        if(otherAdjuster == null)
        {
            return;
        }

        otherAdjuster.UpdateSprite();

    }
}
