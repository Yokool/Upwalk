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
    }
}
